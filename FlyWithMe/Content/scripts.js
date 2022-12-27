$(function () {
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
$(function () {
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

$(function () {

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
    if (!validateForm(form3)) {
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

    // check if any required fields are empty
    const emptyFields = requiredFieldsArray.filter((field) => !field.value);
    if (emptyFields.length > 0) {
        // at least one required field is empty, return false
        return false;
    }
    return true;
}



function hideLoadingScreen() {
    // get the loading screen element
    const loadingScreen = document.getElementById('loading-screen');

    // hide the loading screen by setting the display style to "none"
    loadingScreen.style.display = 'none';

    // reset the flag in sessionStorage to "false"
    sessionStorage.setItem('loadingScreenShown', 'false');
}
function handlePageLoad() {
    // check the value of the loadingScreenShown flag in sessionStorage
    const loadingScreenShown = sessionStorage.getItem('loadingScreenShown');

    // if the flag is set to "true", hide the loading screen
    if (loadingScreenShown === 'true') {
        hideLoadingScreen();
    }
}
window.addEventListener('load', handlePageLoad);
