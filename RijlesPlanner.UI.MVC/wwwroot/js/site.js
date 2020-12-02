// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function GetAllLessons() {
    var events = [];
    $.ajax({
        type: "GET",
        url: "/Lesson/GetAllLessons",
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
            $.ajax({
                type: "GET",
                url: "/Lesson/GetLessonInfoPartialView",
                data: { id: info.event.id },
                datatype: "json",
                cache: false,
                error: function (e) { alert("nope" + e); },
                success: function (partialViewData) {
                    $('#your-div-to-hold-partial-view').html(partialViewData);
                    $('#lessonInfoModal').modal('show');
                }
            });
        },

    })
    calendar.render();
}

function CreateLesson() {
    var title = document.getElementById('Title').value;
    var description = document.getElementById('Description').value;
    var startDate = document.getElementById('StartDate').value;
    var endDate = document.getElementById('EndDate').value;
    var studentId = $("#studentSelect option:selected").val();


    var model = {
        "Title": title,
        "Description": description,
        "StartDate": startDate,
        "EndDate": endDate,
        "StudentId": studentId

    }

    $.ajax({
        type: "POST",
        url: "/Lesson/AddLesson",
        data: model,
        datatype: "json",
        cache: false,
        success: function (response) {
            if (response.success) {
                $('#addLessonModal').modal('hide');
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
    var lessonId = $("#lessonId").text()

    $.ajax({
        method: "POST",
        url: "/Lesson/DeleteLesson",
        data: { id: lessonId },
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
        error: function (xhr) {
            alert('Er ging iets mis met het verwijderen van de les.');
        }
    });
}