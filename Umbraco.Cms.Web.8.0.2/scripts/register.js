$(function () {
    'use strict';
    document.changingEmail = false;

    $('.link-back').click(function (e) {
        e.preventDefault();
        var form = $(this).attr('href');
        enableButton($(form).find('.validate'));
        enableInputs(form);
        goToStep(form);
        loadValues(true);
        //if ($('.phone-format').length) {
        //    $('.phone-format').mask(dictionary.phoneFormat);
        //}
    });

    $('.link-data').click(function (e) {
        var form = $(this).attr('href');
        //clearValues(form);
    });

    $('#resend-button, #resend-button-change-email').click(function (e) {
        e.preventDefault();
        loadValues(false);
        var form = document.changingEmail ? $('#resend-email-code-change-email') : $('.resend-email-code');
        disableInputs(form);
        processing(true);

        $.post("/umbraco/surface/registration/verifyemailresendcode",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    processing(false);
                    $('.message-verification').show();
                    setTimeout(function () {
                        $('.message-verification').fadeOut();
                    }, 5 * 1000);
                    //goToStep($('.validate').attr('href'));
                }
                else {
                    //console.log(data.Message);
                    enableButton($('.validate'));
                    enableInputs(form);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    });

    $('#resend-button-reset-password').click(function (e) {
        e.preventDefault();
        loadValues(false);
        var form = $('#resend-email-code-forgot-password');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/account/forgotpasswordsendemail",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    processing(false);
                    $('.message-verification').show();
                    setTimeout(function () {
                        $('.message-verification').fadeOut();
                    }, 5 * 1000);
                    //goToStep($('.validate').attr('href'));
                }
                else {
                    //console.log(data.ServerErrorMessage);
                    enableButton($('.validate'));
                    enableInputs(form);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    });

    $('#verify-email-button').click(function (e) {
        e.preventDefault();
        if ($('#verification-email-code').val().length === 0) {
            $(this).parent().find('.valid-error').show();
        }
        else {
            $(this).parent().find('.valid-error').hide();
            verifyEmail();
        }
    });

    //$('#registration-email').blur(function () {
    //    $('#registration-user').val(this.value);
    //});

    $('input[name=username]').blur(function (e) {
        const onlyNumbers = new RegExp(/^(?![0-9]+$)[A-Za-z0-9_-]{5,30}$/);
        if (onlyNumbers.test(e.currentTarget.value)) {
            $('.error-message-username').hide();
            $(e.currentTarget).removeClass('valid-error-input');
            $(e.currentTarget).removeClass('invalid');
        }
        else {
            $('.error-message-username').show();
            $(e.currentTarget).addClass('valid-error-input');
            $(e.currentTarget).addClass('invalid');
        }
    });

    $('#registration-user').keypress(function (e) {
        var blockSpecialRegex = /[~`!@#$%^&()*\?={}[\]:;,.<>+\/?-]/;
        //var blockSpecialRegex = /[~`!@#$%^&()_*\?={}[\]:;,.<>+\/?-]/;
        var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        //console.log(key);
        if (blockSpecialRegex.test(key)) {
            //if (blockSpecialRegex.test(key) || $.isNumeric(key)) {
            e.preventDefault();
            return false;
        }
    });

    $('#account-confirm-pass-forgot-password').blur(function () {
        var pass = $('#account-newpass-forgot-password').val();
        var confirm = $(this).val();
        if (pass !== confirm) {
            $('.password-nomatch').show();
            return false;
        }
        else {
            $('.password-nomatch').hide();

        }
    });

    $('input[type=tel]').blur(function (e) {
        if ($(this).val() !== '')
            $(this).val(parseInt($(this).val(), 10));
    });

    $('#registration-pass2').blur(function () {
        var pass = $('#registration-pass1').val();
        var confirm = $('#registration-pass2').val();
        if (pass !== confirm) {
            $('.password-nomatch').show();
            return false;
        }
        else {
            $('.password-nomatch').hide();

        }
    });

    $('#account-confirm-pass-forgot-password-email').blur(function () {
        var pass = $('#account-newpass-forgot-password-email').val();
        var confirm = $(this).val();
        if (pass !== confirm) {
            $('.password-nomatch-email').show();
            return false;
        }
        else {
            $('.password-nomatch-email').hide();

        }
    });

    $('.resend-code').click(function (e) {
        e.preventDefault();
        if ($('.resend-code').attr('disabled')) {
            return;
        }
        $('.resend-code').attr('disabled', 'disabled');
        var form = $('#validate-mobile-number');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/verifymobileresendsms",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    processing(false);
                    enableInputs(form);
                    startTimer(form);
                }
                else {
                    //console.log(data.Message);
                    enableButton($('.validate'));
                    enableInputs(form);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    });

    $('.resend-code-mobile').click(function (e) {
        e.preventDefault();
        if ($('.resend-code-mobile').attr('disabled')) {
            return;
        }
        $('.resend-code-mobile').attr('disabled', 'disabled');
        var form = $('#mobile-verification-confirm-form');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/verifymobileresendsms",
            form.serialize(),
            function (data) {
                if (data.Success) {
                    hideServerError(form);
                    processing(false);
                    enableInputs(form);
                    startTimerMobile(form);
                }
                else {
                    enableButton($('.validate'));
                    enableInputs(form);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                enableButton($('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    });

    $('.resend-code-email').click(function (e) {
        e.preventDefault();
        var self = this;
        if ($(self).attr('disabled')) {
            return;
        }
        $(self).attr('disabled', 'disabled');
        var form = $('#resend-email-code-confirmation-page');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/verifyemailresendcode",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    goToStep($(self).attr('href'));
                    processing(false);
                }
                else {
                    //console.log(data.Message);
                    enableButton($(form).find('.validate'));
                    enableInputs(form);
                    processing(false);
                    if (data.Errors !== undefined) {
                        showServerError(form, data.Errors);
                    }
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($(form).find('.validate'));
                enableInputs(form);
                processing(false);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
            });
    });

    $('.change-email-link').click(function () {
        document.changingEmail = true;
        saveEmail();
        goToStep($(this).attr('href'));
        $('.validate').removeAttr('disabled');
        $('input[name=mobile]').val($('#mobile-number-holder').val());
        $('select[name=countryCode]').val($('#mobile-code-holder').val()).trigger('change');
    });

    $('.validate').click(function (event) {
        $("input").unmask();
        var valid = validate(event);
        if (valid) {
            var method = jQuery(event.currentTarget).data("method");
            if (method !== undefined) {
                var form = '#' + jQuery(event.currentTarget).data('form');
                runMethod(method, form);
            }
        }
        else {
            reEnableButton(event);
            enableInputs($(event.currentTarget).data('form'));
        }
    });

    $('select').each(function (index, value) {
        if ($(value).find('option:selected').val() !== "") {
            $(value).val($(value).find('option:selected').val());
            $(value).parent().find('.select-selected').removeClass('none-selected');
            $(value).trigger('change');
        }
    });

    function runMethod(method, form) {
        switch (method) {
            case "sendSms": sendSms();
                break;
            case "verifyMobile": verifyMobile();
                break;
            case "verifyMobileConfirm": verifyMobileConfirm();
                break;
            case "saveMobile": saveMobile(form, true);
                break;
            case "verifySms": verifySms();
                break;
            case "register": register();
                break;
            case "changeEmail": changeEmail();
                break;
            case "sendResetPassword": sendResetPassword();
                break;
            case "changePassword": changePassword();
                break;
            case "forgotUsername": forgotUsername();
                break;
            case "changePasswordEmail": changePasswordEmail();
                break;
        }
    }

    function changePasswordEmail() {
        var form = $('#change-password-email');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/account/changepasswordviaemail",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    $(form).find('.response-failure').hide();
                    $(form).find('.response-success').show();
                    hideServerError(form);
                    processing(false);
                    setTimeout(function () {
                        document.location.href = "/";
                    }, 5 * 1000);
                }
                else {
                    //console.log(data.Message);
                    enableButton($('.validate'));
                    enableInputs(form);
                    $(form).find('.response-failure').show();
                    $(form).find('.response-success').hide();
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($('.validate'));
                enableInputs(form);
                $(form).find('.response-failure').show();
                $(form).find('.response-success').hide();
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function forgotUsername() {
        event.preventDefault();
        var form = $('#forgot-username');
        disableInputs(form);
        processing(true);
        var mobile = parseInt($(form).find('[name=mobile]').val(), 10);
        var email = $(form).find('[name=email]').val();
        var url = '';
        if (email !== undefined && email !== '') {
            url = 'forgotusernameemail';
        }
        else if (mobile !== undefined && mobile !== '') {
            url = 'forgotusernamesms';
        }

        $.post("/umbraco/surface/account/" + url,
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    goToStep($(form).find('.validate').attr('href'));
                    hideServerError(form);
                    $(form).find('.error-message').hide();

                    //$(form).find('.response-success').show();
                    //$('.back-to-login').show();
                    //setTimeout(function () {
                    //    goToStep($(form).find('.validate').attr('href'));
                    //}, 3 * 1000);
                    //processing(false);
                }
                else {
                    //console.log(data.Payload);
                    //console.log(data.Message);
                    enableButton($(form).find('.validate'));
                    enableInputs(form);
                    $(form).find('.response-success').hide();
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                    $('.back-to-login').hide();

                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($(form).find('.validate'));
                enableInputs(form);
                $(form).find('.response-success').hide();

                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function changePassword() {
        var form = $('#forgot-password-validate-mobile-and-change');
        disableInputs(form);
        processing(true);

        $.post("/umbraco/surface/account/changepasswordviasms",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    $('.error-message').hide();
                    saveMobile(form, false);
                    saveUsername($(form).find('[name=username]').val());
                    hideServerError(form);
                    processing(false);
                    goToStep($(form).find('.validate').attr('href'));
                }
                else {
                    //console.log(data.ServerErrorMessage);
                    enableButton($('.validate'));
                    enableInputs(form);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function changeEmail() {
        var form = $('#change-email-form');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/changeemail",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    processing(false);
                    saveEmail();
                    goToStep($(form).find('.validate').attr('href'));
                    document.changingEmail = false;
                }
                else {
                    //console.log(data.ServerErrorMessage);
                    enableButton($(form).find('.validate'));
                    enableInputs(form);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($(form).find('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function sendResetPassword() {
        var form = $('#reset-password');
        disableInputs(form);
        var mobile = parseInt($(form).find('[name=mobile]').val(), 10);
        var email = $(form).find('[name=email]').val();
        saveEmail();
        saveMobile($('#reset-password'), false);

        if (email !== '') {
            sendResetPasswordEmail();
        }
        else if (mobile !== '') {
            sendResetPasswordSms();
        }
    }

    function sendResetPasswordSms() {
        var form = $('#reset-password');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/account/forgotpasswordsendsms",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    saveMobile(form, false);
                    saveUsername($(form).find('[name=username]').val());
                    goToStep('#forgot-password-verify-sms');
                    $('#verification-code-forgot-password').val("");
                    processing(false);
                }
                else {
                    //console.log(data.Message);
                    enableButton($('.validate'));
                    enableInputs(form);
                    processing(false);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($('.validate'));
                enableInputs(form);
                processing(false);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
            });
    }

    function sendResetPasswordEmail() {
        var form = $('#reset-password');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/account/forgotpasswordsendemail",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    saveEmail($(form).find('[name=email]').val());
                    saveUsername($(form).find('[name=username]').val());
                    goToStep('#forgot-password-verification-emeil--you');
                    processing(false);
                }
                else {
                    //console.log(data.Message);
                    enableButton($(form).find('.validate'));
                    enableInputs(form);
                    processing(false);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($(form).find('.validate'));
                enableInputs(form);
                processing(false);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
            });
    }

    function register() {
        var form = $('#registration-step-3');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/register",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    processing(false);
                    if (data.Payload !== undefined && data.Payload !== undefined && data.Payload.IsActive) {
                        goToStep('#registration-successful');
                    }
                    else {
                        saveEmail();
                        saveId(data.RelatedResponse.Payload.Id);
                        saveUsername(data.RelatedResponse.Payload.Username);
                        goToStep($(form).find('.validate').attr('href'));
                    }
                }
                else {
                    //console.log(data.Message);
                    enableButton($(form).find('.validate'));
                    enableInputs(form);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($(form).find('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function sendSms() {
        var form = document.changingEmail ? $('#insert-mobile-number-change-email') : $('#insert-mobile-number');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/verifymobilesendsms",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(form);
                    processing(false);
                    saveMobile(form, false);
                    goToStep($(form).find('.validate').attr('href'));
                    $('#verification-code-change-email').val("");
                }
                else {
                    //console.log(data.Message);
                    enableButton($('.validate'));
                    enableInputs(form);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function verifyMobile() {
        var form = $('#mobile-verification-form');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/verifymobilesendsms",
            form.serialize(),
            function (data) {
                hideServerError(form);
                processing(false);
                saveMobile(form, false);
                goToStep($('#mobile-verification-confirm'));
                
            }).fail(function (data) {
                enableButton($('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function verifyMobileConfirm() {
        var form = $('#mobile-verification-confirm-form');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/verifymobilevalidatesms2",
            form.serialize() + "&token=" + document.token,
            function (data) {
                if (data.Success) {
                    hideServerError(form);
                    processing(false);
                    Cookies.set('username', document.username, { expires: document.expires, path: '/' });
                    Cookies.set('token', document.token, { expires: document.expires, path: '/' });
                    Cookies.set('lastLogin', document.lastLogin, { expires: document.expires, path: '/' });
                    document.location.reload(true);
                }
                else {
                    enableInputs(form);
                    enableButton($(form).find('.validate'));
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                    return false;
                }
            }).fail(function (data) {
                enableButton($(form).find('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function verifySms() {
        var form = document.changingEmail ? $('#validate-mobile-number-change-email') : $('#validate-mobile-number');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/verifymobilevalidatesms",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    if (document.changingEmail) {
                        saveEmail();
                    }
                    hideServerError(form);
                    processing(false);
                    // go to next step
                    goToStep($(form).find('.validate').attr('href'));
                    $('#registration-change-email').val('');
                }
                else {
                    //console.log(data.Message);
                    enableInputs(form);
                    enableButton($(form).find('.validate'));
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                    return false;
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton($(form).find('.validate'));
                enableInputs(form);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function verifyEmail() {
        var form = $('#validate-email-code');
        disableInputs(form);
        processing(true);
        $.post("/umbraco/surface/registration/verifyemailconfirm",
            form.serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    hideServerError(form);
                    $('#verificat-code-email-done').show();
                    $('#verificat-code-email').hide();
                }
                else {
                    //console.log(data.Message);
                    if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                    processing(false);
                    return false;
                }
            }).fail(function (data) {
                //console.log(data);
                if (data.Errors !== undefined) { showServerError(form, data.Errors); }
                processing(false);
            });
    }

    function saveMobile(form, nextStep) {
        $('#mobile-holder').val($(form).find('select').find('option:checked').val() + parseInt($(form).find('[type=tel]').val(), 10));
        $('#mobile-code-holder').val($(form).find('select').find('option:checked').val());
        $('#mobile-number-holder').val(parseInt($(form).find('[type=tel]').val(), 10));
        if (nextStep) {
            // go to next step
            goToStep($(form).find('.validate').attr('href'));
        }
    }

    function saveEmail(value) {
        var input = '';
        if (value !== undefined) {
            input = value;
        }
        else {
            input = document.changingEmail ? $('#registration-change-email').val() : $('#registration-email').val();
        }
        $('#email-holder').val(input);
    }

    function saveId(id) {
        $('#id-holder').val(id);
    }

    function saveUsername(username) {
        $('#username-holder').val(username);
    }

    function goToStep(step) {
        $('.email-holder').html($('#email-holder').val());
        $('.mobile-holder').html($('#mobile-code-holder').val() + $('#mobile-number-holder').val());
        $(step).modal();
        enableButton($('.validate'));
        enableInputs();
        //clearValues(step);
        loadValues(false);
    }

    function loadValues(back) {
        $('input[name=email]').val($('#email-holder').val());
        $('input[name=mobile]').val($('#mobile-holder').val());
        if (back) {
            $('input[name=mobile]').val($('#mobile-number-holder').val());
            $('select[name=countryCode]').find('option[value="' + $('#mobile-code-holder').val() + '"]').attr('selected', 'selected');
            $('select[name=countryCode]').val($('#mobile-code-holder').val()).trigger('change');
        }
        $('input[name=id]').val($('#id-holder').val());
        $('input[name=username]').val($('#username-holder').val());
    }

    $(document).find('form').on('keypress', function (e) {
        if (e.which === 13) {
            $(this).find('.validate').click();
        }
    });
});

$.fn.isBound = function (type) {
    var data = $._data($(this)[0], 'events');

    if (data[type] === undefined || data.length === 0) {
        return false;
    }
    else {
        return true;
    }
};

function clearValues(form) {
    $(form).find('input[type=text],input[type=tel],input[type=number],input[type=email],input[type=file],input[type=image],input[type=password],select:not([data-prepopulated]),textarea').val("");
    $(form).find('select').find('option').removeAttr('selected');
    $(form).find('select').val('').trigger('change');
    $(form).find('input,select,textarea').removeAttr('readonly');
    $(form).find('.validate').removeAttr('disabled');
}

function highlightInputError(element) {
    $(element).addClass('valid-error-input');
    element.scrollIntoView();
}

function removeHightlight(element) {
    $(element).removeClass('valid-error-input');
}

function reEnableButton(e) {
    $(e.currentTarget).removeAttr('disabled');
}

function enableButton(element) {
    element.removeAttr('disabled');
}

function enableInputs(form) {
    if (form !== undefined)
        $(form).find('input,select,textarea').removeAttr('readonly');
    else
        $('input,select,textarea').removeAttr('readonly');
}

function disableInputs(form) {
    $(form).find('input,select,textarea').attr('readonly', 'readonly');
}

function showServerError(form, errors) {
    var serverErrorTag = $(form).find('.server-error');
    var error = '';
    var code = '';
    if (errors !== null && errors[0] !== undefined) {
        for (var i = 0; i < errors.length; i++) {
            error += $('#error-' + errors[i].ErrorCode).val();
            code = errors[i].ErrorCode;
        }
    }
    else if (errors !== null) {
        code = errors.ErrorCode;
        error = $('#error-' + code).val();
        if (error === undefined) error = errors.ErrorMessage;
    }

    if (error !== '') {
        $(serverErrorTag).html(error);
        // Display the error
        $(serverErrorTag).show();
    }
    assertServerError(form, code);
}

function hideServerError(form) {
    var serverErrorTag = $(form).find('.server-error');
    $(serverErrorTag).html("");
    $(serverErrorTag).hide();
    hideAllInputErrors(form);
}

function assertServerError(form, code) {
    var error = null;
    code = parseInt(code);
    switch (code) {
        case 111:
        case 142:
            error = $(form).find('input[name=username]');
            break;
        case 113:
        case 122:
        case 123:
        case 124:
        case 131:
        case 137:
            error = $(form).find('input[name=email]');
            break;
        case 116:
            error = $(form).find('input[name=email],input[name=mobile]');
            break;
        case 136:
        case 139:
            error = $(form).find('input[name=mobile]');
            break;
        case 133:
        case 134:
        case 138:
        case 140:
            error = $(form).find('select[name=code]');
            break;
        case 114:
            error = $(form).find('select[name=day]');
            break;
        case 117:
            error = $(form).find('select[name=country]');
            break;
        case 118:
            error = $(form).find('select[name=currency]');
            break;
        case 119:
            error = $(form).find('select[name=language]');
            break;
        case 120:
            error = $(form).find('select[name=timezone]');
            break;
        case 126:
            error = $(form).find('input[name=password]');
            break;
        case 127:
            error = $(form).find('input[name=oldpassword]');
            break;
    }
    if (error !== null && $(error).data('validation-message') !== '') {
        var text = $(form).find('.' + $(error).data('validation-message')).html();
        $(form).find('.server-error').append(`<br/><span>${text}</span>`);
        //$('.' + $(error).data('validation-message')).show();
    }
}

function hideAllInputErrors(form) {
    $(form).find('.error-message').hide();
}

function processing(flag) {
    if (flag === true) {
        $('.processing').show();
    }
    else if (flag === false) {
        $('.processing').hide();
    }
}


// validation
function validate(e) {
    var clickable = $(e.currentTarget).attr('disabled') === undefined;
    if (clickable) {
        e.preventDefault();
        $(e.currentTarget).attr('disabled', 'disabled');
        var isValid = [];
        var hasValue = [];
        var formId = $(e.currentTarget).data('form');
        if (formId !== undefined) {
            var form = $('#' + formId);
            // disable all inputs temporarily
            disableInputs(form);
            var validateTarget = $(form).find('.validate-target');
            validateTarget.each(function (index, item) {
                var validateExpression = $(item).data('validate-expression');
                var noValue = '.' + $(item).data('no-value');
                if ($(item).hasClass("invalid")) {
                    isValid[index] = false;
                    reEnableButton(e);
                    enableInputs(form);
                    highlightInputError(item);
                    return;
                }
                if ($(item).attr('type') === 'checkbox' && $(item).attr('required') !== undefined) {
                    if (!$(item).is(':checked')) {
                        if (noValue.length > 1) {
                            $(noValue).show();
                        }
                        reEnableButton(e);
                        enableInputs(form);
                        hasValue[index] = false;
                        highlightInputError(item);
                    }
                    else {
                        if (noValue.length > 1) {
                            $(noValue).hide();
                        }
                        hasValue[index] = true;
                        removeHightlight(item);
                    }
                    return;
                }
                if ($(item).is('select') && $(item).attr('required') !== undefined) {
                    if ($(item).find('option:selected').val() === "") {
                        reEnableButton(e);
                        enableInputs(form);
                        hasValue[index] = false;
                        highlightInputError(item);
                    }
                    else {
                        if (noValue.length > 1) {
                            $(noValue).hide();
                        }
                        hasValue[index] = true;
                        removeHightlight(item);
                    }
                }
                if ($(item).val().length === 0 && $(item).attr('required') !== undefined) {
                    if (noValue.length > 1) {
                        $(noValue).show();
                    }
                    reEnableButton(e);
                    enableInputs(form);
                    hasValue[index] = false;
                    highlightInputError(item);
                }
                else {
                    if (noValue.length > 1) {
                        $(noValue).hide();
                    }
                    hasValue[index] = true;
                    removeHightlight(item);
                }
                if (validateExpression.length !== 0 && $(item).val().length > 0) {
                    var re = new RegExp(validateExpression, 'gm');
                    var result = re.test($(item).val().toLowerCase());
                    var msg = '.' + $(item).data('validation-message');
                    if (!$(item).hasClass('duo-required')) {
                        if (result === false) {
                            if (msg.length > 1) {
                                $(msg).show();
                            }
                            reEnableButton(e);
                            enableInputs(form);
                            isValid[index] = false;
                            highlightInputError(item);
                        }
                        else {
                            if (msg.length > 1) {
                                $(msg).hide();
                            }
                            isValid[index] = true;
                            removeHightlight(item);
                        }
                    }
                    else {
                        isValid[index] = true;
                    }
                }
                else if ($(item).hasClass('duo-required')) {
                    var bothEmpty = [];
                    $(form).find('.duo-required').each(function (index, element) {
                        if ($(element).val() === undefined || $(element).val() === null || $(element).val() === '')
                            bothEmpty[index] = false;
                        else bothEmpty[index] = true;
                    });
                    var checker = array => array.every(Boolean);
                    if (checker(bothEmpty)) isValid[index] = false;
                    else isValid[index] = true;
                }
                else if ($(item).val().length === 0) {
                    var errMsg = '.' + $(item).data('validation-message');
                    if (errMsg.length > 1) {
                        $(errMsg).hide();
                    }
                }
                else {
                    isValid[index] = true;
                    removeHightlight(item);
                }
            });
            var hasMissingValue = hasValue.includes(false, 0);
            var hasInvalid = isValid.includes(false, 0);
            if (!hasMissingValue && !hasInvalid) {
                $('.server-error').hide();
                return true;
            }
            else {
                enableInputs(form);
                return false;
            }
        }
    }
}

// timer
function startTimer(form) {
    var waitTime = 60;
    $('.resend-time').show();
    var timer = setInterval(function () {
        waitTime--;
        $(form).find('.wait-time').html(waitTime);
        if (waitTime === 0) {
            clearInterval(timer);
            $('.resend-code').removeAttr('disabled');
            $('.resend-time').hide();
        }
    }, 1 * 1000);
}
function startTimerMobile(form) {
    var waitTime = 60;
    $('.resend-time-mobile').show();
    var timer = setInterval(function () {
        waitTime--;
        $(form).find('.wait-time').html(waitTime);
        if (waitTime === 0) {
            clearInterval(timer);
            $('.resend-code-mobile').removeAttr('disabled');
            $('.resend-time-mobile').hide();
        }
    }, 1 * 1000);
}