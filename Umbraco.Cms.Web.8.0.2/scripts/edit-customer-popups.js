app.controller("AccountEditPopups", accountEditPopups);

function accountEditPopups() {
    var vm = this;
    vm.email = '';
    vm.mobileNumber = '';

    $('#confirm-password-change').blur(function () {
        var pass = $('input[name="newpassword"]').val();
        var confirm = $(this).val();
        if (pass !== confirm) {
            $('.password-nomatch').show();
            return false;
        }
        else {
            $('.password-nomatch').hide();

        }
    });

    vm.submitChangePassword = function (event) {
        var valid = validate(event);
        if (valid) {
            var form = '#' + $(event.currentTarget).data('form');
            var data = {
                "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
                "tenantUid": $(form).find('[name=tenantUid]').val(),
                "oldpassword": $(form).find('[name=oldpassword]').val(),
                "newpassword": $(form).find('[name=newpassword]').val(),
                "username": Cookies.get('username'),
                "customerToken": Cookies.get('token')
            };
            var url = '/umbraco/surface/account/editpassword';
            post(url, data);
            enableInputs(form);
        }
    };

    vm.submitChangeUsername = function (event) {
        var valid = validate(event);
        if (valid) {
            var form = '#' + $(event.currentTarget).data('form');
            //var validationRequired = $(form).find('[name=validation-required]').val();
            var data = {};
            var url = '';
            vm.username = $(form).find('[name=username]').val();

            data = {
                "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
                "username": $(form).find('[name=username]').val(),
                "tenantUid": $(form).find('[name=tenantUid]').val(),
                "customerGuid": settings.customerGuid,
                "customerToken": Cookies.get('token'),
                "language": $(form).find('[name=language]').val()
            };
            url = '/umbraco/surface/account/EditCustomerUsername';
            post(url, data, form);
            enableInputs(form);
        }
    };
    vm.submitChangeCommPreferences = function (event) {
        var valid = validate(event);
        if (valid) {
            var form = '#' + $(event.currentTarget).data('form');
            var data = {
                "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
                "tenantUid": $(form).find('[name=tenantUid]').val(),
                "notify": $(form).find('[name=notify]').is(':checked'),
                "notifyViaPlatform": $(form).find('[name=notifyViaPlatform]').is(':checked'),
                "notifyViaEmail": $(form).find('[name=notifyViaEmail]').is(':checked'),
                "notifyViaSms": $(form).find('[name=notifyViaSms]').is(':checked'),
                "username": Cookies.get('username'),
                "customerToken": Cookies.get('token')
            };
            var url = '/umbraco/surface/account/editcommunicationpreferences';
            post(url, data);
            enableInputs(form);
        }
    };
    vm.submitChangeEmail = function (event) {
        var valid = validate(event);
        if (valid) {
            var form = '#' + $(event.currentTarget).data('form');
            //var validationRequired = $(form).find('[name=validation-required]').val();
            var data = {};
            var url = '';
            vm.email = $(form).find('[name=email]').val();
            //if (validationRequired === "true") {
            //    data = {
            //        "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
            //        "email": $(form).find('[name=email]').val(),
            //        "tenantUid": $(form).find('[name=tenantUid]').val(),
            //        "language": $(form).find('[name=language]').val()
            //    };
            //    url = '/umbraco/surface/registration/verifyemailsendcode';
            //}
            //else {
            //    data = {
            //        "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
            //        "email": $(form).find('[name=email]').val(),
            //        "tenantUid": $(form).find('[name=tenantUid]').val(),
            //        "username": Cookies.get('username'),
            //        "customerToken": Cookies.get('token')
            //    };
            //    url = '/umbraco/surface/account/editemail';
            //}
            data = {
                "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
                "email": $(form).find('[name=email]').val(),
                "tenantUid": $(form).find('[name=tenantUid]').val(),
                "username": Cookies.get('username'),
                "customerToken": Cookies.get('token'),
                "language": $(form).find('[name=language]').val()
            };
            url = '/umbraco/surface/account/editemail';
            post(url, data, form);
            enableInputs(form);
        }
    };
    vm.resendVerificationEmail = function (event) {
        var form = '#' + $(event.currentTarget).data('form');
        var url = '/umbraco/surface/registration/verifyemailresendcode';
        var data = {
            "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
            "email": vm.email,
            "tenantUid": $(form).find('[name=tenantUid]').val(),
            "language": $(form).find('[name=language]').val()
        };
        post(url, data, form, 'email');
    };
    vm.submitChangeMobile = function (event) {
        var valid = validate(event);
        if (valid) {
            var form = '#' + $(event.currentTarget).data('form');
            //var validationRequired = $(form).find('[name=validation-required]').val();
            var url = '';
            var data = {};
            vm.mobileNumber = $(form).find('[name=countryCode]').find('option:selected').val() + $(form).find('[name=mobile]').val();
            //if (validationRequired === "true") {
            //    data = {
            //        "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
            //        "tenantUid": $(form).find('[name=tenantUid]').val(),
            //        "countryCode": $(form).find('[name=countryCode]').find('option:selected').val(),
            //        "mobile": $(form).find('[name=mobile]').val(),
            //        "language": $(form).find('[name=language]').val()
            //    };
            //    url = '/umbraco/surface/registration/verifymobilesendsms';
            //}
            //else {
            //    data = {
            //        "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
            //        "tenantUid": $(form).find('[name=tenantUid]').val(),
            //        "countryCode": $(form).find('[name=countrycode]').val(),
            //        "mobile": $(form).find('[name=mobile]').val(),
            //        "username": Cookies.get('username'),
            //        "customerToken": Cookies.get('token')
            //    };
            //    url = '/umbraco/surface/register/editmobilenumber';
            //}
            data = {
                "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
                "tenantUid": $(form).find('[name=tenantUid]').val(),
                "countryCode": $(form).find('[name=countryCode]').find('option:selected').val(),
                "mobile": $(form).find('[name=mobile]').val(),
                "username": Cookies.get('username'),
                "customerToken": Cookies.get('token')
            };
            url = '/umbraco/surface/account/editmobilenumber';
            post(url, data, form);
            enableInputs(form);
        }
    };
    vm.resendVerificationSms = function (event) {
        var form = '#' + $(event.currentTarget).data('form');
        var url = '/umbraco/surface/registration/verifymobileresendsms';
        var data = {
            "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
            "mobile": vm.mobileNumber,
            "tenantUid": $(form).find('[name=tenantUid]').val(),
            "language": $(form).find('[name=language]').val()
        };
        post(url, data, form, 'sms');
    };
    vm.validateSms = function (event) {
        var form = '#' + $(event.currentTarget).data('form');
        var url = '/umbraco/surface/registration/verifymobilevalidatesms';
        var data = {
            "__RequestVerificationToken": $(form).find('[name="__RequestVerificationToken"]').val(),
            "mobile": vm.mobileNumber,
            "code": $(form).find('[name=code]').val(),
            "tenantUid": $(form).find('[name=tenantUid]').val(),
            "language": $(form).find('[name=language]').val()
        };
        post(url, data, form);
    };
    vm.check = function (event) {
        var isChecked = $(event.currentTarget).attr('checked');
        if (isChecked) {
            $(event.currentTarget).removeAttr('checked');
        }
        else {
            $(event.currentTarget).attr('checked', 'checked');
        }
    };
    vm.forceRefresh = function () {
        var date = new Date();
        var lastLogin = new Date(parseInt(Cookies.get('lastLogin').substr(6)));
        var lastLoginUtc = new Date(lastLogin.getUTCFullYear(), lastLogin.getUTCMonth(), lastLogin.getUTCDate(), lastLogin.getUTCHours(), lastLogin.getUTCMinutes(), lastLogin.getUTCSeconds());
        var loginInfo = {
            "username": Cookies.get('username'),
            "customerToken": Cookies.get('token'),
            "lastLogin": lastLoginUtc,
            "tenantUid": $('#tenantUid-root').val(),
            "nowUtc": new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds())
        };
        var data = {
            "token": loginInfo.customerToken,
            "tenantUid": loginInfo.tenantUid
        };
        $.post("/umbraco/surface/account/refreshtoken",
            data,
            function (response) {
                //console.log(response);
                if (response.Success) {

                    Cookies.set('username', response.Credential, { expires: response.Expires, path: '/' });
                    Cookies.set('token', response.Token, { expires: response.Expires, path: '/' });
                    Cookies.set('lastLogin', response.LastLogin, { expires: response.Expires, path: '/' });

                    countdown = response.Expires * 60;
                }
                else {
                    console.log("refresh failed");
                    $('#logout-button').click(); // refresh failed
                }
            });
        setTimeout(function () {
            location.reload();
        }, 2 * 1000);
    };

    $('.link-back-edit').click(function (e) {
        e.preventDefault();
        var form = $(this).attr('href');
        enableButton($(form).find('.validate'));
        enableInputs(form);
        $(form).modal();
    });

    function post(url, data, form, type) {
        processing(true);
        $.post(url,
            data,
            function (data) {
                //console.log(data);
                if (data.Success) {
                    $('.response-success').show();
                    $('.response-failure').hide();
                    enableButton($(event.srcElement));
                    setTimeout(function () {
                        $('.response-success').fadeOut();
                    }, 6 * 1000);
                    processing(false);
                    if (form !== undefined) {
                        if (type !== undefined && type === 'sms') {
                            startTimer(form);
                        }
                        if (type !== undefined && type === 'email') {
                            $(form).find('.message-verification').show();
                            setTimeout(function () {
                                $(form).find('.message-verification').fadeOut();
                            }, 5 * 1000);
                        }
                        var step = $(form).find('.validate').attr('href');
                        if (step !== undefined) {
                            $(step).modal();
                        }
                        $(form).find('.server-error').hide();
                    }
                    $('.server-error').hide();
                }
                else {
                    //console.log(data.Message);
                    $('.response-success').hide();
                    //$('.response-failure').show();
                    if (data.Errors !== undefined) {
                        showServerError(form, data.Errors);
                    }
                    if ($(self).find('.response-failure').html() !== undefined) {
                        var error = $(self).find('.response-failure').html();
                        $('.server-error').append(`<br/>${error}`);
                    }
                    enableButton($(event.srcElement));
                    processing(false);
                }
            }).fail(function (data) {
                enableButton($(event.srcElement));
                if (data.Errors !== undefined) {
                    showServerError(form, data.Errors);
                }
                processing(false);
            });
    }
}