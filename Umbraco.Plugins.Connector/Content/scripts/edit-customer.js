app.controller("EditCustomer", editCustomer);

function editCustomer() {
    var vm = this;
    vm.selectGender = function (event) {
        var form = $(event.currentTarget).parents('form:first');
        $(form).find('[name=gender]').removeAttr('checked');
        $(event.currentTarget).attr('checked', 'checked');
    };

    vm.submit = function (event) {
        processing(true);
        var valid = validate(event);
        if (valid) {
            var form = '#' + $(event.currentTarget).data('form');
            var data = {
                "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
                "firstname": $(form).find('[name=firstname]').val(),
                "lastname": $(form).find('[name=lastname]').val(),
                "gender": $(form).find('[name=gender]:checked').length ? $(form).find('[name=gender]:checked').val() : $(form).find('[name=gender]').val(),
                "title": $(form).find('[name=title]').val(),
                "countryCode": $(form).find('[name=countrycode]').val(),
                //"currency": $(form).find('[name=currency]').val(),
                "language": $(form).find('[name=language]').val(),
                "odds": $(form).find('[name=odds]').val(),
                "tenantUid": $(form).find('[name=tenantUid]').val(),
                "day": $(form).find('[name=day]').val(),
                "month": $(form).find('[name=month]').val(),
                "year": $(form).find('[name=year]').val(),
                "customerGuid": $(form).find('[name=customerGuid]').val(),
                /*"username": Cookies.get('username'),*/
                "username": $(form).find('[name=Username]').val(),
                "customerToken": Cookies.get('token'),
                "address1": $(form).find('[name=address1]').val(),
                "address2": $(form).find('[name=address2]').val(),
                "address3": $(form).find('[name=address3]').val(),
                "postalcode": $(form).find('[name=postalcode]').val(),
                "town": $(form).find('[name=town]').val(),
                "country": $(form).find('[name=country]').val(),
                "timezone": $(form).find('[name=timezone]').val(),
                "notify": $(form).find('[name=notify]').val()
            };
            $.post("/umbraco/surface/account/editcustomer",
                data,
                function (response) {
                    //console.log(response);
                    if (response.Success) {
                        //console.log(response);
                        $('.response-success').show();
                        $('.response-failure').hide();
                        $(form).find('.server-error').hide();
                        enableButton($(event.srcElement));
                        setTimeout(function () {
                            $('.response-success').fadeOut();
                        }, 6 * 1000);
                        processing(false);
                    }
                    else {
                        //console.log(response.Message);
                        $('.response-success').hide();
                        //$('.response-failure').show();
                        if (response.Errors !== undefined) {
                            showServerError(form, response.Errors);
                        }
                        var error = $(self).find('.response-failure').html();
                        $('.server-error').append(`<br/>${error}`);
                        enableButton($(event.srcElement));
                        processing(false);
                    }
                }).fail(function (response) {
                    //console.log(response);
                    enableButton($(event.srcElement));
                    if (response.Errors !== undefined) {
                        showServerError(form, response.Errors);
                    }
                    processing(false);
                });
        }
    };
}