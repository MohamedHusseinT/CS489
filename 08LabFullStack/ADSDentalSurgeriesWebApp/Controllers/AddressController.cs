using Microsoft.AspNetCore.Mvc;
using ADSDentalSurgeriesWebApp.Services;
using ADSDentalSurgeriesWebApp.ViewModels;

namespace ADSDentalSurgeriesWebApp.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: Address
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var addresses = string.IsNullOrWhiteSpace(searchTerm) 
                ? await _addressService.GetAllAddressesAsync()
                : await _addressService.SearchAddressesAsync(searchTerm);

            var viewModel = new AddressListViewModel
            {
                Addresses = addresses,
                SearchTerm = searchTerm,
                TotalCount = addresses.Count
            };

            return View(viewModel);
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _addressService.CreateAddressAsync(addressViewModel);
                    TempData["SuccessMessage"] = "Address created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the address: {ex.Message}");
                }
            }

            return View(addressViewModel);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddressViewModel addressViewModel)
        {
            if (id != addressViewModel.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedAddress = await _addressService.UpdateAddressAsync(id, addressViewModel);
                    if (updatedAddress == null)
                    {
                        return NotFound();
                    }

                    TempData["SuccessMessage"] = "Address updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while updating the address: {ex.Message}");
                }
            }

            return View(addressViewModel);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _addressService.DeleteAddressAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Address deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Address not found or could not be deleted.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the address: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
