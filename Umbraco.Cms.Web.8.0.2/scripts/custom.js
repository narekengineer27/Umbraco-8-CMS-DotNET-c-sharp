function updateClock(offset) {
    Date.prototype.addMinutes = function (m) {
        this.setTime(this.getTime() + (m * 60 * 1000));
        return this;
    };

    var now = new Date();
    var utcTime = new Date(now.getTime() + (now.getTimezoneOffset() * 60 * 1000));

    var currentTime = utcTime;
    currentTime = currentTime.addMinutes(offset);
    // Operating System Clock Hours for 24h clock
    var currentHours = currentTime.getHours();
    // Operating System Clock Minutes
    var currentMinutes = currentTime.getMinutes();
    // Operating System Clock Seconds
    var currentSeconds = currentTime.getSeconds();
    // Adding 0 if Hours, Minutes & Seconds is More or Less than 10
    currentHours = (currentHours < 10 ? "0" : "") + currentHours;
    currentMinutes = (currentMinutes < 10 ? "0" : "") + currentMinutes;
    currentSeconds = (currentSeconds < 10 ? "0" : "") + currentSeconds;
    // Picking "AM" or "PM" 12h clock if time is more or less than 12

    // display first 24h clock and after line break 12h version
    var currentTimeString = currentHours + ":" + currentMinutes + ":" + currentSeconds + "";
    // print clock js in div #clock.
    $("#clock").html(currentTimeString);
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode !== 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function AutoUpdateAnonymous(currentPageDocType, CurrentPageID, currentLanguage, bgColor, popupWidth) {
    var url = '/umbraco/surface/account/getanonymoushubconnectionasync';
    var data = {
        "tenantUid": settings.tenantUid
    };
    $.post(url, data,
        function (response) {

            this.hub = new signalR.HubConnectionBuilder()
                .withUrl(response.Url, {
                    accessTokenFactory: () => response.Token
                })
                .build();

            this.hub.start();

            this.hub.on("InPlatformMessage", (response) => {
                if (response) {
                    NotificationMessageAnonymous(response, currentLanguage, bgColor, popupWidth);
                }
            });
        });
}
function AutoUpdate(currentPageDocType, CurrentPageID, currentLanguage, bgColor, popupWidth) {
    var url = '/umbraco/surface/account/gethubconnectionasync';
    var data = {
        "userId": settings.customerGuid,
        "tenantUid": settings.tenantUid,
        "customerToken": settings.customerToken
    };
    $.post(url, data,
        function (response) {

            this.hub = new signalR.HubConnectionBuilder()
                .withUrl(response.Url, {
                    accessTokenFactory: () => response.Token
                })
                .build();

            this.hub.start();

            this.hub.on("BalanceUpdate", (response) => {
                if (response) {
                    document.getElementById("update_balance").click();
                }
            });
            this.hub.on("InPlatformMessage", (response) => {
                if (response) {
                    NotificationMessage(response, currentLanguage, bgColor, popupWidth);
                }
            });

            if (currentPageDocType === "totalCodeTicketPage") {
                this.hub.on("TicketResponseUpdate", (response) => {
                    UpdateTicketComments(response);
                });
            }
        });
}
function NotificationMessage(response, crrntLanguage, bgColor, popupWidth) {
    if (crrntLanguage === response.Language && response.UserId === settings.customerGuid) {
        toastr.info(response.Content, response.Title, undefined, bgColor, popupWidth);
    }
}
function NotificationMessageAnonymous(response, crrntLanguage, bgColor, popupWidth) {
    if (crrntLanguage === response.Language) {
        toastr.info(response.Content, response.Title, undefined, bgColor, popupWidth);
    }
}

$(document).ready(function () {
    var hash = window.location.hash.substring(1);
    if (hash == "popup-login") {
        history.replaceState(null, null, ' ');
        $("#popup-login").modal("show");
    }
	
    if (Cookies.get('accessToken') === undefined || Cookies.get('accessToken') === 'null') {
        var inHour = 1 / 24;
        var data = {
            "tenantUid": settings.tenantUid
        };
        $.post("/umbraco/surface/account/getaccesstoken", data,
            function (response) {
                Cookies.set('accessToken', response, { path: '/', expires: inHour });
            });
    }

    setInterval(function () {
        var offset = parseInt($('#global-timezone-selector').val());
        updateClock(offset);
    }, 1000);

    $('#date-of-birth').one('mouseover', function () {
        window.Date = window.JDate;
        flatpickr("#date-of-birth", {
            enableTime: false,
            "locale": "fa"
        });
    });

    flatpickr("#date-of-birth", {
        enableTime: false,
        "locale": "fa"
    });


});

