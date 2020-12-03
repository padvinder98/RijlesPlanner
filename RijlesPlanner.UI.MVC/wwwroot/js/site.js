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

$(document).on('click', '#add-lesson-button', function () {
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
});

$(document).on('click', '#delete-lesson', function () {
    var r = confirm("Weet u zeker dat u deze les wilt verwijderen?");
    if (r == true) {
        txt = DeleteLesson();
    }
});

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

$(document).on('click', '#student-info', function () {

    var currentRow = $(this).closest("tr");

    var id = currentRow.find("td:eq(0)").text();

    $.ajax({
        type: "GET",
        url: "/Student/GetStudentInfoPartialView",
        data: { id: id },
        datatype: "json",
        cache: false,
        error: function (e) { alert("nope" + e); },
        success: function (partialViewData) {
            $('#partial-holder').html(partialViewData);
            $('#studentInfoModal').modal('show');
        }
    });
});


$(document).on('click', '#get-add-lesson', function () {
    $.ajax({
        type: "GET",
        url: "/Lesson/GetAddLessonPartialView",
        error: function (e) { alert("nope" + e); },
        success: function (partialViewData) {
            $('#your-div-to-hold-partial-view').html(partialViewData);
            $('#addLessonModal').modal('show');
        }
    });
});

$(document).on('click', '#add-student', function () {

    var currentRow = $(this).closest("tr");

    var id = currentRow.find("td:eq(0)").text();

    $.ajax({
        type: "POST",
        url: "/Dashboard/AddStudentToInstuctor",
        data: { studentId: id },
        datatype: "json",
        cache: false,
        success: function (response) {
            if (response.success) {
                if (response.responseText != null) {
                    alert(response.responseText);
                    window.location.href = "/Dashboard/Students";
                }
            } else {
                alert(response.responseText);
            }
        },
        error: function (xhr) {
            alert('Er ging iets mis met het toevoegen van de student.');
        }
    });
});

$(document).on('click', '#remove-student', function () {

    var currentRow = $(this).closest("tr");

    var id = currentRow.find("td:eq(0)").text();

    $.ajax({
        type: "POST",
        url: "/Dashboard/RemoveStudentFromInstructor",
        data: { studentId: id },
        datatype: "json",
        cache: false,
        success: function (response) {
            if (response.success) {
                if (response.responseText != null) {
                    alert(response.responseText);
                    window.location.href = "/Dashboard/Students";
                }
            } else {
                alert(response.responseText);
            }
        },
        error: function (xhr) {
            alert('Er ging iets mis met het verwijderen van de student.');
        }
    });
});