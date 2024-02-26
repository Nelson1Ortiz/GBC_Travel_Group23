document.addEventListener('DOMContentLoaded', function () {
    // Tab switching functionality
    var tabs = document.querySelectorAll('.tabbed-search .tab-nav a');
    tabs.forEach(function (tab) {
        tab.addEventListener('click', function (event) {
            event.preventDefault();
            var activeTabs = document.querySelectorAll('.active');

            // Deactivate existing active tab and panel
            activeTabs.forEach(function (activeTab) {
                activeTab.classList.remove('active');
            });

            // Activate new tab and panel
            tab.classList.add('active');
            document.querySelector(tab.getAttribute('href')).classList.add('active');
        });
    });

    // Toggle return date input based on trip type selection
    var roundTripInput = document.getElementById('round-trip');
    var oneWayInput = document.getElementById('one-way');
    var returnDateDiv = document.querySelector('.return-date');

    function toggleReturnDateInput() {
        if (oneWayInput && oneWayInput.checked) {
            returnDateDiv.style.display = 'none';
        } else {
            returnDateDiv.style.display = 'block';
        }
    }

    if (roundTripInput) roundTripInput.addEventListener('change', toggleReturnDateInput);
    if (oneWayInput) oneWayInput.addEventListener('change', toggleReturnDateInput);

    toggleReturnDateInput(); // Set initial state of the return date input

    // Initialize datepicker for departure and return dates with linked selection
    $('#departure-date').datepicker({
        onSelect: function (selectedDate) {
            // Set the minDate for return-date picker to the selected departure date
            $('#return-date').datepicker('option', 'minDate', selectedDate);
            // Optionally open the return date picker
            $('#return-date').datepicker('show');
        },
        numberOfMonths: 2,
        showButtonPanel: true
    });

    $('#return-date').datepicker({
        numberOfMonths: 2,
        showButtonPanel: true
    });

    // Initialize datepicker for hotel and car rental date inputs
    $("#hotel-check-in, #hotel-check-out, #car-rental-pick-up-date, #car-rental-drop-off-date").datepicker();
});