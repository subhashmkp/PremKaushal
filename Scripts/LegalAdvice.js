
       $(document).ready(function () {
        
           $('#btnAnswer').on('click', function() {
               //function loadQA() {
               var $btn = $(this).button('loading');
               // business logic...
               var email = $('#InputEmail2').val();

               $.ajax({
                   url: 'LegalAdvice/getAnswerbyEmail/?email='+email,
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
                       
                       $('#QA').html(
                           '<h1>Legal Advice</h1><hr/>'
                           );
                       if (obj.length>0) {
                           $('#QA').append(
                               '<h3>Welcome '+obj[0].Question.Contact.Name+'</h3><br/>'
                          );
                           for (var i = 0; i < obj.length; i++) {
                               $('#QA').append(
                                   '<div class="panel panel-primary"><div class="panel-heading"><h3 class="panel-title">' + obj[i].Question.Subject + '</h3></div>' +
                                       '<div class="panel-body">' +
                                       '<div class="alert alert-danger">' +
                                       '<p>' + obj[i].Question.Question1 + '</p>' +
                                       '</div>' +
                                       '<div class="alert alert-success">' +
                                       '<p>' + obj[i].Answer1 + '</p>' +
                                       '</div>' +
                                       '</div>' +
                                       '</div>'
                               );
                           }
                       } else {
                           $('#QA').append('<h3>User not found. Please check the entered email or Ask a new Question.</h3><br/>');
                       }
                       //for (var i = 0; i < obj.length; i++) {
                       //    alert(obj[i].Question.Subject);
                       //    alert(obj[i].Question.Question1);
                       //    alert(obj[i].Answer1);
                       //}
                       //alert('Success');
                   },
                   error: function(xhr, exception) {
                       alert('Error: ' + xhr.statusText);
                       $('#QA').html('<h3>'+xhr.statusText+'</h3>');
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

               $btn.button('reset');
           });
       });    
