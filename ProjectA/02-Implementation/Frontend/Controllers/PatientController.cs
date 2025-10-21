using Microsoft.AspNetCore.Mvc;
using ADSDentalSurgeriesWebApp.Services;
using ADSDentalSurgeriesWebApp.ViewModels;

namespace ADSDentalSurgeriesWebApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patient
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var patients = string.IsNullOrWhiteSpace(searchTerm) 
                ? await _patientService.GetAllPatientsAsync()
                : await _patientService.SearchPatientsAsync(searchTerm);

            var viewModel = new PatientListViewModel
            {
                Patients = patients,
                SearchTerm = searchTerm,
                TotalCount = patients.Count
            };

            return View(viewModel);
        }

        // GET: Patient/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patient/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new PatientViewModel
            {
                AvailableAddresses = await _patientService.GetAvailableAddressesAsync()
            };

            return View(viewModel);
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _patientService.CreatePatientAsync(patientViewModel);
                    TempData["SuccessMessage"] = "Patient created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the patient: {ex.Message}");
                }
            }

            patientViewModel.AvailableAddresses = await _patientService.GetAvailableAddressesAsync();
            return View(patientViewModel);
        }

        // GET: Patient/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            patient.AvailableAddresses = await _patientService.GetAvailableAddressesAsync();
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientViewModel patientViewModel)
        {
            if (id != patientViewModel.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedPatient = await _patientService.UpdatePatientAsync(id, patientViewModel);
                    if (updatedPatient == null)
                    {
                        return NotFound();
                    }

                    TempData["SuccessMessage"] = "Patient updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while updating the patient: {ex.Message}");
                }
            }

            patientViewModel.AvailableAddresses = await _patientService.GetAvailableAddressesAsync();
            return View(patientViewModel);
        }

        // GET: Patient/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _patientService.DeletePatientAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Patient deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Patient not found or could not be deleted.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the patient: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
