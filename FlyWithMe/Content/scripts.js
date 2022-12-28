
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

