// Morris.js Charts sample data for SB Admin template
$(document).ready(function() {
    $(function() {
        var tmessages = 0, tcontacts = 0, tquestions = 0, tanswers = 0;
        $.ajax({
            url: 'Admin/getCount/',
            type: 'POST',
            //data: JSON.stringify(email.data),
            //data: { 'EMail' : email },
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function(result) {
                //alert(result);
                var obj;
                obj = JSON.parse(result);
                //alert(obj.Subject + obj.Id + obj.Question1);
                //alert(obj.length);
                //if (obj.length > 0) {
                    tmessages = obj.TMessages;
                    tcontacts = obj.TContacts;
                    tquestions = obj.TQuestions;
                    tanswers = obj.TAnswers;

                    $('#newMsg').html(obj.Messages);
                    $('#newContact').html(obj.Contacts);
                    $('#newQuestion').html(obj.Questions);
                    $('#newLog').html(obj.Logs);
                //}
                //alert(tmessages);
                // Donut Chart
                    Morris.Donut({
                        element: 'morris-donut-chart',
                        data: [{
                            label: "Contacts",
                            value: tcontacts
                        }, {
                            label: "Messages",
                            value: tmessages
                        }, {
                            label: "Questions",
                            value: tquestions
                        }, {
                            label: "Answers",
                            value: tanswers
                        }],
                        resize: true
                    });
            },
            error: function(xhr, exception) {
                alert('Error: ' + xhr.statusText);
                if (xhr.status === 0) {
                    alert('Not connect.\n Verify Network.');
                } else if (xhr.status == 404) {
                    alert('Requested page not found. [404]');
                } else if (xhr.status == 500) {
                    alert('Internal Server Error [500].' + exception.toString());
                } else if (exception === 'parsererror') {
                    alert('Requested JSON parse failed.');
                } else if (exception === 'timeout') {
                    alert('Time out error.');
                } else if (exception === 'abort') {
                    alert('Ajax request aborted.');
                } else {
                    alert('Uncaught Error.\n' + xhr.responseText);
                }
            },

            async: true,
            processData: false
        });       
    });
});