


$(document).ready(function () {

    var dates = $(".FullOrdered");
    var datesFormated = [];
    for (let i = 0; i < dates.length; i++) {
        datesFormated[i] = new Date(dates[i].value).getFullYear() + '-' + (new Date(dates[i].value).getMonth() + 1) + '-' + ('0' + new Date(dates[i].value).getDate()).slice(-2);
    }

   

    $('input[name=daterange]').daterangepicker({
        changeMonth: true,
        changeYear: true,
        minDate: new Date(),
        maxDate: moment().add(12, 'month'),
        "isInvalidDate": function (date) {
            calender_date = date._d.getFullYear() + '-' + (date._d.getMonth() + 1) + '-' + ('0' + date._d.getDate()).slice(-2);

            var search_index = $.inArray(calender_date, datesFormated);

            if (search_index > -1) {

                return true;
            }
            else {
                return false;
            }      
        },
        isCustomDate: function (date) {
            calender_date = date._d.getFullYear() + '-' + (date._d.getMonth() + 1) + '-' + ('0' + date._d.getDate()).slice(-2);

            var search_index = $.inArray(calender_date, datesFormated);

            if (search_index > -1 && date._d> new Date() ) {

                return ["highlighted-cal-dates"];

            }
            else {
                return false;
            }     
        }
    });

    

    $('#sb_but').click(function (ev) {
  
        var p1 = $("#start_place").val($("#start_p").find(":selected").val());
        var p2 = $("#end_place").val($("#end_p").find(":selected").val());
       
        $("#start_place").replaceWith(p1);
        $("#end_place").replaceWith(p2);
        for (var i = new Date($("#start_time")[0].value.replace("-", "/")); i <= new Date($("#end_time")[0].value.replace("-", "/")); i.setDate(i.getDate()+1)) {
            var dForm = i.getFullYear() + '-' + (i.getMonth() + 1) + '-' + ('0' + i.getDate()).slice(-2);
            if ($.inArray(dForm, datesFormated) > -1) {
                $("#datepickerError")[0].innerHTML = "Selected range could not contain fully boocked days";
                $("#datepickerError").addClass("text-danger");
                ev.preventDefault();
                return;
            };
        };
    });
    $('input[name=daterange]').on('apply.daterangepicker', function (ev, picker) {
        console.log(picker.startDate.format('YYYY-MM-DD'));
        console.log(picker.endDate.format('YYYY-MM-DD'));

        var s1 = $("#start_time").value=picker.startDate.format('YYYY-MM-DD');
        var s2 = $("end_time").val(picker.endDate.format('YYYY-MM-DD'));

        document.getElementById("StartTime").value = picker.startDate.format('YYYY-MM-DD');
        document.getElementById("EndTime").value = picker.endDate.format('YYYY-MM-DD');

        for (var i = picker.startDate; i <= picker.endDate; i.add(1, "days")) {
           var dForm= i._d.getFullYear() + '-' + (i._d.getMonth() + 1) + '-' + ('0' + i._d.getDate()).slice(-2);
            if ($.inArray(dForm, datesFormated) > -1) {
                $("#datepickerError")[0].innerHTML = "Selected range could not contain fully boocked days";
                $("#datepickerError").addClass("text-danger");
                ev.preventDefault();
                return;
            }
            else {
                $("#datepickerError")[0].innerHTML = "";
            }
        };
    });

});