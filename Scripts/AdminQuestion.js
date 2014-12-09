$(document).ready(function () {
    //function getQuesbyCid() {
     $('.btnModalA').on('click', function () {
    //$('#question').on('click', '.btnModalQ', function () {
        var qid = $(this).attr('name');
        $.ajax({
            url: 'getAnswersbyQid/?qid=' + qid,
            type: 'POST',
            //data: JSON.stringify(email.data),
            //data: { 'EMail' : email },
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                //alert(result);
                var obj;
                obj = JSON.parse(result);
                //alert(obj.Subject + obj.Id + obj.Question1);
                //alert(obj.Answer.Answer1);
                $('#myModalBody').html("");
                $('#myModalLabel').html("Question Id: ");
                //if (obj.length > 0) {
                    //for (var i = 0; i < obj.length; i++) {
                        //var dt = (obj[i].CreatedDate).split('T');
                        // if (obj[i].Type == "Error") {
                        $('#myModalLabel').append(
                            obj.Question.Id 
                        );
                        $('#myModalBody').append(
                                        '<b class="text-warning"><span class="badge">Subject</span> ' + obj.Question.Subject + '</b>' +
                                       '<p class="text-danger"><span class="badge">Question</span> ' + obj.Question.Question1 + '</p>' +
                                       '<span class="badge">Answer</span> ' +
                                            '<form action="insertAnswer" method="post">'+
                                            '<input class="form-control" type="text" name="answer" value="' + obj.Answer1 + '">' +
                                            '<input type="hidden" name="qid" value="' + obj.Question.Id + '">' +
                                            '<input type="hidden" name="cname" value="' + obj.Question.Contact.Name + '">' +
                                            '<input type="hidden" name="email" value="' + obj.Question.Contact.Email + '">' +
                                            '<button type="submit" class="btn btn-default">Submit</button></form>'
                        );
                        // }
                   // }
                //}
                //alert('Success');
            },
            error: function (xhr, exception) {
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

    //$('#contact').on('click', '.btnModalC', function () {
     $('.btnModalC').on('click', function () {
        var cid = $(this).attr('name');
        $.ajax({
            url: 'getContact/?cid=' + cid,
            type: 'POST',
            //data: JSON.stringify(email.data),
            //data: { 'EMail' : email },
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                //alert(result);
                var obj;
                obj = JSON.parse(result);
                //alert(obj.Subject + obj.Id + obj.Question1);
                //alert(obj.Answer.Answer1);
                $('#myModalBody').html("");
                $('#myModalLabel').html("Contact Id: ");
              // if (obj.length > 0) {
                 //   for (var i = 0; i < obj.length; i++) {
                        //var dt = (obj[i].CreatedDate).split('T');
                        // if (obj[i].Type == "Error") {
                        $('#myModalLabel').append(
                            obj.Id
                        );
                        $('#myModalBody').append(
                                        '<p class="text-success"><span class="badge">Name</span> ' + obj.Name + '</p>' +
                                        '<p class="text-info"><span class="badge">Email</span> ' + obj.Email + '</p>' +
                                        '<p class="text-info"><span class="badge">Contact No</span> ' + obj.ContactNo + '</p>'+
                                        '<p class="text-info"><span class="badge">Status</span> ' + obj.Status + '</p>'
                        );
                        // }
                  //  }
               // }
                //alert('Success');
            },
            error: function (xhr, exception) {
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