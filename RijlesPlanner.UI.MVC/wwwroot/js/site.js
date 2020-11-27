// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function GetAllLessons() {
    var events = [];
    $.ajax({
        type: "GET",
        url: "/Dashboard/GetAllLessons",
        dataType: 'json',
        success: function (data) {
            $.each(data, function (Key, Value) {
                events.push({
                    id: Value.id,
                    title: Value.title,
                    description: Value.description,
                    start: Value.start,
                    end: Value.end,
                });
            })
            GenerateCalender(events);
        },
        error: function (error) {
            alert('failed');
        }
    })
}

function GenerateCalender(events) {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        locale: 'nl',
        headerToolbar: {
            left: 'prevYear,prev,next,nextYear',
            center: 'title',
            right: 'today dayGridMonth,timeGridWeek,timeGridDay'
        },
        editable: true,
        selectable: true,
        dayMaxEvents: true, // allow "more" link when too many events
        allDaySlot: false,
        events: events,
        contentHeight: 'auto',


        eventClick: function (info) {
            $('#myModal #eventTitle').text(info.event.title);
            $('#myModal #eventId').text(info.event.id);

            var $description = $('<div/>');
            $description.append($('<p/>').html('<b>Start tijd: </b>' + info.event.start));
            if (info.event.end != null) {
                $description.append($('<p/>').html('<b>Eind tijd: </b>' + info.event.end));
            }
            $description.append($('<p/>').html('<b>Opmerking:</b>' + info.event.extendedProps.description));
            $('#myModal #pDetails').empty().html($description);

            $('#myModal').modal();
        },

    })
    calendar.render();
}

function CreateLesson() {
    var title = document.getElementById('Title').value;
    var description = document.getElementById('Description').value;
    var startDate = document.getElementById('StartDate').value;
    var endDate = document.getElementById('EndDate').value;

    var model = {
        "Title": title,
        "Description": description,
        "StartDate": startDate,
        "EndDate": endDate
    }

    $.ajax({
        type: "post",
        url: "/Dashboard/AddLesson",
        data: model,
        datatype: "json",
        cache: false,
        success: function (response) {
            if (response.success) {
                GetAllLessons();
                if (response.responseText != null) {
                    alert(response.responseText);
                }
            } else {
                alert(response.responseText);
            }
        },
        error: function (status) {
            alert("Er ging iets mis met het toevoegen van de les.")
        },
    });
}

function DeleteLesson() {
    var id = $("#eventId").text()

    $.ajax({
        method: "post",
        url: "/Schedule/DeleteLesson",
        data: JSON.stringify(id),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.success) {
                GetAllLessons();
                if (response.responseText != null) {
                    alert(response.responseText);
                }
            } else {
                alert(response.responseText);
            }
        },
        error: function (xhr) {
            alert('Er ging iets mis met het verwijderen van de les.');
        }
    });
}