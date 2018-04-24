using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WeddingSite.Auth;
using WeddingSite.Models;

namespace WeddingSite.Controllers
{
    public class GuestController : Controller
    {
        [GuestCodeAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Guest/
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }

        [Authorize]
        public ActionResult RegisterGuest()
        {
            bool authorized = false;

            var identity = Thread.CurrentPrincipal as ClaimsPrincipal;

            if (Session["GuestCodeValidated"] == null || !(bool)Session["GuestCodeValidated"])
            {
                if (identity.HasClaim(c => c.Type == "GuestCode"))
                {
                    using (var db = new WeddingManagementContext())
                    {
                        string guestCode = identity.FindFirst("GuestCode").Value.ToUpper();

                        authorized = db.GuestCodes.Any(gc => gc.GuestCode1.ToUpper() == guestCode && gc.UseLimit > 0);
                    }
                }
                else
                {
                    using (var db = new WeddingManagementContext())
                    {
                        int userId = User.Identity.GetUserId<int>();

                        UserClaim guestCodeClaim = db.Users.FirstOrDefault(u => u.Id == userId).Claims.FirstOrDefault(c => c.ClaimType == "GuestCode");

                        if (guestCodeClaim != null)
                        {
                            authorized = db.GuestCodes.Any(gc => gc.GuestCode1.ToUpper() == guestCodeClaim.ClaimValue.ToUpper() && gc.UseLimit > 0);
                        }
                    }
                }
            }
            else
            {
                authorized = true;
            }

            if (authorized && Session["RegisterGuestReturn"] != null)
            {
                return Redirect(Session["RegisterGuestReturn"].ToString());
            }
            else if (authorized)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult RegisterGuest(RegisterGuestViewModel vm)
        {
            var db = new WeddingManagementContext();
            var userManager = new UserManager<User, int>(new UserStore<User, UserRole, int, UserLogin, UserUserRole, UserClaim>(new WeddingManagementContext()));

            string requestedGuestCode = (vm.GuestCode ?? string.Empty).ToUpper();

            GuestCode dbCode = db.GuestCodes.FirstOrDefault(gc => gc.GuestCode1.ToUpper() == requestedGuestCode);

            if (dbCode == null)
            {
                ModelState.AddModelError("GuestCode", "Guest code not found!");
            }
            else if (db.Users.Count(u => u.Claims.Any(c => c.ClaimType == "GuestCode" && c.ClaimValue == requestedGuestCode)) > dbCode.UseLimit)
            {
                ModelState.AddModelError("GuestCode", "Exceeded use allowance!");
            }

            if (ModelState.IsValid)
            {
                // TODO: Save guest code claim.
                // TODO: Redirect to requested url in session.

                userManager.AddClaim(User.Identity.GetUserId<int>(), new Claim("GuestCode", dbCode.GuestCode1));
                var identity = new ClaimsIdentity(User.Identity);
                identity.AddClaim(new Claim("GuestCode", dbCode.GuestCode1));

                if (Session["RegisterGuestReturn"] != null)
                {
                    return Redirect(Session["RegisterGuestReturn"].ToString());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(vm);
            }
        }

        [Auth.GuestCodeAuthorize]
        public ActionResult RSVP()
        {
            var vm = new RsvpViewModel();

            var identity = Thread.CurrentPrincipal as ClaimsPrincipal;

            string guestCode = null;
            GuestCode dbCode = null;

            using (var db = new WeddingManagementContext())
            {

                if (identity.HasClaim(c => c.Type == "GuestCode"))
                {
                    guestCode = identity.FindFirst("GuestCode").Value.ToUpper();

                    dbCode = db.GuestCodes.FirstOrDefault(gc => gc.GuestCode1.ToUpper() == guestCode);
                }
                else
                {
                    int userId = User.Identity.GetUserId<int>();

                    UserClaim guestCodeClaim = db.Users.FirstOrDefault(u => u.Id == userId).Claims.FirstOrDefault(c => c.ClaimType == "GuestCode");

                    if (guestCodeClaim != null)
                    {
                        dbCode = db.GuestCodes.FirstOrDefault(gc => gc.GuestCode1.ToUpper() == guestCodeClaim.ClaimValue.ToUpper());
                    }
                }


                if (dbCode != null)
                {
                    vm.GuestCode = dbCode.GuestCode1;

                    if (dbCode.GuestId != null)
                    {
                        Guest dbGuest = db.Guests.FirstOrDefault(g => g.GuestId == dbCode.GuestId);

                        if (dbGuest != null)
                        {
                            vm.Name = dbGuest.Name;

                            vm.AllowedGuests = dbGuest.MaxAllowed;

                            if (dbGuest.RSVPFlag)
                            {
                                vm.Attending = dbGuest.NumberAttending >= 0;
                            }

                            if (dbGuest.NumberAttending >= 0)
                            {
                                vm.NumberOfGuests = dbGuest.NumberAttending;
                            }
                        }
                    }
                    else
                    {
                        // TODO: Consider redirecting to register guest if the code isn't attached to a guest.
                    }
                }
            }

            return View(vm);
        }

        [HttpPost]
        [GuestCodeAuthorize]
        public ActionResult RSVP(RsvpViewModel viewModel)
        {
            var identity = Thread.CurrentPrincipal as ClaimsPrincipal;

            string guestCode = null;
            GuestCode dbCode = null;
            Guest dbGuest = null;

            using (var db = new WeddingManagementContext())
            {
                viewModel.GuestCode = (viewModel.GuestCode ?? string.Empty).ToUpper();

                dbCode = db.GuestCodes.FirstOrDefault(gc => gc.GuestCode1.ToUpper() == viewModel.GuestCode);

                if (dbCode != null && dbCode.GuestId != null)
                {
                    dbGuest = db.Guests.FirstOrDefault(g => g.GuestId == dbCode.GuestId);

                    if (dbGuest != null)
                    {
                        if (viewModel.Attending == true)
                        {
                            if (viewModel.NumberOfGuests <= 0)
                            {
                                ModelState.AddModelError("NumberOfGuests", "We need to know how many people are attending.");
                            }

                            if (viewModel.NumberOfGuests > dbGuest.MaxAllowed)
                            {
                                ModelState.AddModelError("NumberOfGuests", "Too many people attending.");
                            }
                        }
                        else if (viewModel.Attending == null)
                        {
                            ModelState.AddModelError("Attending", "We need to know if you plan to attend.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("GuestCode", "No guest found for this guest code.");
                    }
                }
                else
                {
                    ModelState.AddModelError("GuestCode", "Invalid guest code.");
                }

                if (ModelState.IsValid)
                {
                    db.Entry(dbGuest).State = System.Data.Entity.EntityState.Modified;

                    dbGuest.RSVPFlag = true;

                    if (viewModel.Attending == true)
                    {
                        dbGuest.NumberAttending = viewModel.NumberOfGuests;
                    }
                    else
                    {
                        dbGuest.NumberAttending = -1;
                    }

                    db.SaveChanges();

                    return View("RSVP_Confirmed", viewModel);
                }
                else
                {
                    return View(viewModel);
                }
            }
        }
    }
}