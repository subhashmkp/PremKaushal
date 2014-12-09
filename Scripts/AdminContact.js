$(document).ready(function () {
    $.ajax({
        url: 'getContacts/',
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

            if (obj.length > 0) {
                for (var i = 0; i < obj.length; i++) {
                    //var dt = (obj[i].CreatedDate).split('T');
                   // if (obj[i].Type == "Error") {
                        $('#contact').append(
                            '<div class="col-lg-4">' +
                                '<div class="panel panel-default">' +
                                    '<div class="panel-heading">' +
                                        '<h3 class="panel-title">'
                                            + obj[i].Id + '</h3>' +
                                    '</div>' +
                                '<div class="panel-body">' +
                                    '<div class="list-group">' +
                                        '<div class="list-group-item"><span class="badge">Name</span>'
                                            + obj[i].Name + '</div>' +
                                '<div class="list-group-item"><span class="badge">Email</span>'
                                            + obj[i].Email + '</div>' +
                                '<div class="list-group-item"><span class="badge">Contact No.</span>'
                                            + obj[i].ContactNo + '</div>' +
                                '<button type="button" name="'+obj[i].Id + '" class="btn btn-default btnModalM" data-toggle="modal" data-target="#myModal">Message</button>' +
                                '<button type="button" name="' + obj[i].Id + '" class="btn btn-danger btnModalQ" data-toggle="modal" data-target="#myModal">Question</button>'
                        );
                   // }
                }
            }
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

    //function getQuesbyCid() {
   // $('.btnModalQ').on('click', function () {
      $('#contact').on('click', '.btnModalQ', function() {
        var cid = $(this).attr('name');
        $.ajax({
            url: 'getAnswers/?cid=' + cid,
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
                //alert(obj.Answer.Answer1);
                $('#myModalBody').html("");
                $('#myModalLabel').html("Question Id: ");
                if (obj.length > 0) {
                    for (var i = 0; i < obj.length; i++) {
                        //var dt = (obj[i].CreatedDate).split('T');
                        // if (obj[i].Type == "Error") {
                        $('#myModalLabel').append(
                            obj[i].Question.Id + ", "
                        );
                        $('#myModalBody').append(
                                        '<b class="text-warning"><span class="badge">Subject</span> ' + obj[i].Question.Subject + '</b>' +
                                       '<p class="text-danger"><span class="badge">Question</span> ' + obj[i].Question.Question1 + '</p>' +
                                       '<p class="text-success"><span class="badge">Answer</span> ' + obj[i].Answer1 + '</p>' +
                                        '<p class="text-info"><span class="badge">Created Date</span> ' + obj[i].CreatedDate + '</p>' +
                                        '<p class="text-info"><span class="badge">Status</span> ' + obj[i].Question.Status + '</p>'
                                        
                        );
                        // }
                    }
                }
                //alert('Success');
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
    
      $('#contact').on('click', '.btnModalM', function () {
          var cid = $(this).attr('name');
          $.ajax({
              url: 'getMessages/?cid=' + cid,
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
                  $('#myModalLabel').html("Message Id: ");
                  if (obj.length > 0) {
                      for (var i = 0; i < obj.length; i++) {
                          //var dt = (obj[i].CreatedDate).split('T');
                          // if (obj[i].Type == "Error") {
                          $('#myModalLabel').append(
                              obj[i].Id + ", "
                          );
                          $('#myModalBody').append(
                                          '<p class="text-success"><span class="badge">Message</span> ' + obj[i].Msg + '</p>' +
                                          '<p class="text-info"><span class="badge">Status</span> ' + obj[i].Status + '</p>'

                          );
                          // }
                      }
                  }
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