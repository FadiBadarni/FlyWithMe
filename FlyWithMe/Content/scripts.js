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



