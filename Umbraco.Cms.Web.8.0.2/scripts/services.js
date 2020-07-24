app.service('ticketService', function ($http) {
    const API_URL = '/umbraco/surface/ticket';

    this.createTicket = function (ticket, tenantUid) {
        return $http.post(`${API_URL}/createTicket?tenantuid=${tenantUid}`, { ticket: ticket });
    };

    this.createTicketAnonymous = function (ticketAnonymous, tenantUid) {
        return $http.post(`${API_URL}/createTicketAnonymous?tenantuid=${tenantUid}`, { ticket: ticketAnonymous });
    };

    this.closeTicket = function (ticket, tenantUid) {
        return $http.post(`${API_URL}/closeTicket?tenantUid=${tenantUid}`, { ticket: ticket });
    };

    this.createMessage = function (message, tenantUid) {
        return $http.post(`${API_URL}/createmessage?tenantUid=${tenantUid}`, { message: message });
    };
});

app.service('fileService', function (Upload) {
    const API_URL = '/umbraco/surface/ticketfile';

    this.uploadAnonymous = function (file, tenantUid) {
        return Upload.upload({
            url: `${API_URL}/UploadAnonymous?tenantUid=${tenantUid}`,
            data: { file: file }
        });
    };

    this.upload = function (file, tenantUid) {
        return Upload.upload({
            url: `${API_URL}/Upload?tenantUid=${tenantUid}`,
            data: { file: file }
        });
    };
});

app.service('cardService', function ($http) {
    const API_URL = '/umbraco/surface/card';

    this.addCard = function (card) {
        return $http.post(`${API_URL}/AddCard?tenantUid=${settings.tenantUid}`, card);
    };

    this.addIban = function (card) {
        return $http.post(`${API_URL}/AddIban?tenantUid=${settings.tenantUid}`, card);
    };

    this.updateCard = function (card) {
        return $http.post(`${API_URL}/UpdateCard?tenantUid=${settings.tenantUid}`, card);
    };

    this.activeCards = function () {
        return $http.get(`${API_URL}/ActiveCards?tenantUid=${settings.tenantUid}&pageId=${settings.pageId}`);
    };
	
    this.activeCardsAll = function () {
        return $http.get(`${API_URL}/ActiveCards?tenantUid=${settings.tenantUid}`);
    };

    this.getCards = function () {
        return $http.get(`${API_URL}/GetCards?tenantUid=${settings.tenantUid}`);
    };

    this.deleteCard = function (cardNumber) {
        return $http.post(`${API_URL}/DeleteCard?tenantUid=${settings.tenantUid}`, { cardNumber: cardNumber });
    };
});

app.service('transactionService', function ($http) {
    const API_URL = '/umbraco/surface/transaction';

    this.deposit = function (deposit) {
        return $http.post(`${API_URL}/Deposit?tenantUid=${settings.tenantUid}`, deposit);
    };

    this.withdraw = function (withdraw) {
        return $http.post(`${API_URL}/Withdraw?tenantUid=${settings.tenantUid}`, withdraw);
    };
});

app.service('transactionHistoryService', function ($http) {
    const API_URL = '/umbraco/surface/transactionhistory';

    this.depositTransaction = function (depositTransaction) {
        return $http.post(`${API_URL}/DepositTransaction?tenantUid=${settings.tenantUid}`, depositTransaction);
    };

    this.withdrawTransaction = function (withdrawTransaction) {
        return $http.post(`${API_URL}/WithdrawTransaction?tenantUid=${settings.tenantUid}`, withdrawTransaction);
    };

    this.cancelWithdrawal = function (transactionGuid) {
        return $http.post(`${API_URL}/CancelWithdrawal?tenantUid=${settings.tenantUid}&transactionGuid=${transactionGuid}`);
    };

    this.bonusTransaction = function (bonusTransaction) {
        return $http.post(`${API_URL}/BonusTransaction?tenantUid=${settings.tenantUid}`, bonusTransaction);
    };
});

app.service('tenantService', function ($http) {
    this.getCards = function (rootGuid, customerGuid, customerToken, pageId) {
        return $http.post(`/umbraco/surface/tenant/getcards?rootGuid=${rootGuid}&customerGuid=${customerGuid}&token=${customerToken}&pageId=${pageId}&lang=${settings.language}`);
    };
});

app.service('gameService', function ($http) {
    this.getGameGrid = function (data) {
        return $http.post('/umbraco/surface/game/getlivecasinogrid', data);
    };
});

app.service('utilityService', function ($http, $compile) {

    this.getCurrency = function (baseCurrency, quoteCurrency, paymentSystem) {
        return $http.post(`/umbraco/surface/utility/currencies?tenantUid=${settings.tenantUid}&baseCurrency=${baseCurrency}&quoteCurrency=${quoteCurrency}&paymentSystem=${paymentSystem}`);
    };

    this.alert = function (message, redirectUrl) {
        var template = `<div class="popup-default default-offset zoom-anim-dialog">
                            <form>
                                <div class="height-36"></div>
                                <h3 class="heading-default text-center mb-60">
                                    ${message}
                                </h3>
                                ${redirectUrl ?
                `<div class="wrap--send-button text-center"><a href="${redirectUrl}" class="button send-button">${dictionary.gotIt}</a></div>` :
                `<div class="wrap--send-button text-center"><button class="button send-button" onclick='location.reload()'">${dictionary.gotIt}</button></div>`
            }
                            </form>
                        </div>`;
        $(template).modal();
    };

    this.confirm = function (message, scope) {
        var template = `<div class="popup-default default-offset zoom-anim-dialog">
                            <form>
                                <div class="height-36"></div>
                                <h3 class="heading-default text-center mb-60">
                                    ${message}
                                </h3>
                                <div class="wrap--send-button text-center">
                                    <button class="button send-button mr-20 mb-20" ng-click="confirmYes()">${dictionary.yesCancel}</button>
                                    <button class="button send-button gray-color" ng-click="confirmNo()">${dictionary.no}</button>
                                </div>
                            </form>
                        </div>`;

        scope.confirmNo = function () {
            $.modal.close();
        };

        var angularElement = angular.element(template);
        var compiledTemplate = $compile(angularElement)(scope);

        $(compiledTemplate).modal();
    };
});