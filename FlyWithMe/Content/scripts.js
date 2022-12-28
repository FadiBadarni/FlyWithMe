
// get all the rows in the table
var rows = document.getElementById('flights-table').getElementsByTagName('tr');

// initialize the lowest price and the row with the lowest price
var lowestPrice = Infinity;
var lowestPriceRow = null;

// loop through the rows and find the row with the lowest price
for (var i = 0; i < rows.length; i++) {
    var row = rows[i];
    var price = parseInt(row.getAttribute('data-price'));
    if (price < lowestPrice) {
        lowestPrice = price;
        lowestPriceRow = row;
    }
}
// create a star icon element
var starIcon = document.createElement('i');
starIcon.className = 'fas fa-star';
starIcon.style.marginLeft = '10px'; // add padding to the right of the star icon
// insert the star icon into the first cell of the row with the lowest price
lowestPriceRow.cells[4].appendChild(starIcon);
lowestPriceRow.style.backgroundColor = 'lightyellow';

(function () {
    var dtToday = new Date();


    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var minDate = year + '-' + month + '-' + day;

    $('#startDate').attr('min', minDate);
});
(function () {
    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var minDate = year + '-' + month + '-' + day;

    $('#endDate').attr('min', minDate);
});

(function () {

    $(".flights-table tbody tr").addClass("unfocused");
    $('.unfocused').hover(function () {
        $(this).removeClass('unfocused');
    }, function () {
        $(this).addClass('unfocused');
    });
});

function changeFontColor(color) {
    document.querySelector('.child2').style.color = color;
}



function sortTable() {
    var table, rows, switching, i, x, y, shouldSwitch, sortList, sortOption;
    table = document.getElementById("flights-table");
    sortList = document.getElementById("sortList");
    sortOption = sortList.value;
    if (sortOption === "") {
        return;
    }
    switching = true;
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getAttribute("data-price");
            y = rows[i + 1].getAttribute("data-price");
            if (sortOption === "price-asc") {
                if (x > y) {
                    shouldSwitch = true;
                    break;
                }
            } else if (sortOption === "price-desc") {
                if (x < y) {
                    shouldSwitch = true;
                    break;
                }
            } else if (sortOption === "popularity") {
                x = rows[i].getAttribute("data-popularity");
                y = rows[i + 1].getAttribute("data-popularity");
                if (x < y) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}









const form3 = document.querySelector('.search-form');
form3.addEventListener('submit', handleFormSubmit);

function handleFormSubmit(event) {
    // prevent the default form submission behavior
    event.preventDefault();
    // select the form element
    const form2 = event.target;
    // validate the form input
    if (!validateForm(form2)) {
        // form input is invalid, do not show the loading screen
        return;
    }
    // show the loading screen
    document.querySelector('.loading-screen').style.display = 'block';
    // submit the form after a short delay to allow the loading screen to be displayed
    setTimeout(() => {
        sessionStorage.setItem('loadingScreenShown', 'true');
        form2.submit();
    }, 2000); // the delay duration should be specified here
}

function validateForm(form) {
    // select all required form elements
    const requiredFields = form.querySelectorAll('[required]');
    const requiredFieldsArray = Array.from(requiredFields);
    var passengersCount = document.getElementById('PassengersNumber').value;
    passengersCount = parseInt(passengersCount);
    // check if any required fields are empty
    const emptyFields = requiredFieldsArray.filter((field) => !field.value);
    if (emptyFields.length > 0 || passengersCount > 10 || passengersCount < 1) {
        // at least one required field is empty, return false
        return false;
    }
    return true;
}

window.addEventListener('load', function () {
    // hide the loading screen
    document.getElementById('loading-screen').style.display = 'none';
});