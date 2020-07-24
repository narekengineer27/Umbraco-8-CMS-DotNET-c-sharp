var app = angular.module("Gamingplatformapp", ['ngFileUpload', 'cleave.js']);

app.controller("registrationController", registrationController);
app.controller("transactionController", transactionController);
app.controller("transactionFilterController", transactionFilterController);
app.controller("transactionAllFilterController", transactionAllFilterController);
app.controller("userBalanceController", userBalanceController);
app.controller("createTicketController", createTicketController);
app.controller("createTicketAnonymousController", createTicketAnonymousController);
app.controller("closeTicketController", closeTicketController);
app.controller("closeTicketConfirmController", closeTicketConfirmController);
app.controller("createMessageController", createMessageController);
app.controller("ticketController", ticketController);
app.controller("fileController", fileController);
app.controller("addCardController", addCardController);
app.controller("addCardWithdrawController", addCardWithdrawController);
app.controller("addIbanController", addIbanController);
app.controller("editCardController", editCardController);
app.controller("deleteCardConfirmController", deleteCardConfirmController);
app.controller("depositTransactionHistoryTableController", depositTransactionHistoryTableController);
app.controller("withdrawTransactionHistoryTableController", withdrawTransactionHistoryTableController);
app.controller("allTransactionHistoryTableController", allTransactionHistoryTableController);
app.controller("GameGridController", GameGridController);
app.controller("mobileMenuController", mobileMenuController);
app.controller("allBonusTransactionHistoryTableController", allBonusTransactionHistoryTableController); 
app.controller("allBonusTransactionHistoryFilterController", allBonusTransactionHistoryFilterController); 


app.filter("gamefilter", function () {

    function filter(games, term) {

        var filteredGames = [];
        if (term !== '') {
            filteredGames = games.filter(function (element) {
                if (element.Name === term) return element;
                if (element.SubCategory === term) return element;
                if (element.GameProvider === term) return element;
                if (element.Description !== null && element.Description.includes(term)) return element;
                if (term.Favourite && element.Favourite) return element;
                if (term.NewGame && element.NewGame) return element;
            });
        }
        return filteredGames.length > 0 ? filteredGames : games;
    }

    return filter;
});

function userBalanceController() {
    var vm = this;
    var updating = false;

    vm.showUserBalance = Cookies.get('showBalance') !== undefined ? Cookies.get('showBalance') === "true" : true;

    vm.updateShowBalanceCookie = function (e) {
        Cookies.set('showBalance', vm.showUserBalance, { expires: 999, path: '/' });
    };

    vm.updateBalance = function (e) {
        if (!updating) {
            updating = true;
            var url = '/umbraco/surface/account/getcustomersummaryasync';
            var form = $(e.srcElement).parents('form:first');
            $(form).find('.reload').addClass('is-active');
            var data = {
                "tenantUid": $(form).find('[name=tenantUid]').val(),
                "customerToken": Cookies.get('token')
            };
            processing(true);
            $.post(url, data,
                function (response) {
                    //console.log(response);
                    if (response.Balance !== null) {
                        $('.current-balance,.current-balance-dg').html(response.Balance.CurrentBalance);
                        $('.current-withdrawable').html(response.Balance.Withdrawable);
                        $('.current-bonus').html(response.Balance.Bonus);
                        $('.currency-format').unmask();
                        $('.currency-format').mask(dictionary.currencyFormat, { reverse: true });
                        processing(false);
                        updating = false;
                        $(form).find('.reload').removeClass('is-active');
                        $('.current-balance-dg,.current-withdrawable,.current-bonus').css('color', '#ffc30a');
                        setTimeout(function () {
                            $('.current-balance-dg,.current-withdrawable,.current-bonus').css('color', '#fff');
                        }, 2 * 1000);
                    }
                }).fail(function (response) {
                    //console.log(response);
                    $(form).find('.reload').removeClass('is-active');
                    processing(false);
                    updating = false;
                });
        }
    };
}

function depositTransactionHistoryTableController() {
    var vm = this;
    vm.transactions = [];
}

function withdrawTransactionHistoryTableController(transactionHistoryService, utilityService, $scope) {
    var vm = this;
    vm.transactions = [];
    vm.transactionGuid;

    $scope.confirmYes = function () {
        //console.log(vm.transactionGuid);
        transactionHistoryService.cancelWithdrawal(vm.transactionGuid).then(function () {
            utilityService.alert(dictionary.cancelWithdrawSuccess);
        }, function () {
            //console.log('Error cancelling withdrawal');
        });
    };

    vm.showCancelPopup = function (transactionGuid) {
        //console.log(`cancelpopup - ${transactionGuid}`);
        vm.transactionGuid = transactionGuid;
        utilityService.confirm(dictionary.cancelWithdrawConfirm, $scope);
    };

}

function allTransactionHistoryTableController() {
    var vm = this;
    vm.transactions = [];

}

var transactionTableInitiliazed = false;
function transactionAllFilterController(transactionHistoryService) {
    //console.log('initiating transactionallfiltercontroller');
    var vm = this;

    vm.processing = false;
    vm.filterCount = 0;
    vm.ready = false;

    vm.types = {
        deposit: true,
        withdraw: true
    };

    vm.resetFilters = function () {

        vm.types = {
            deposit: false,
            withdraw: false
        };

        $("#start-date").val('');
        $("#end-date").val('');
        $("#start-date--mob2").val('');
        $("#end-date--mob2").val('');
    };

    vm.applyFilters = function () {
        vm.filterCount = 0;
        var startDate;
        var endDate;

        if (vm.viewPort === 'desktop') {
            startDate = $("#start-date").val();
            endDate = $("#end-date").val();
        }
        else {
            startDate = $("#start-date--mob2").val();
            endDate = $("#end-date--mob2").val();
        }

        if (startDate && endDate) {
            var transactionParam = {
                StartDateTime: startDate,
                EndDateTime: endDate
            };
            vm.filterCount = 2;
            getTransactionHistory(transactionParam);
        }
        else {
            getTransactionHistory(getDefaultParam());
        }
    };


    /// convert date format from mm/dd/yy to dd/mm/yy
    function convertDateFormat(date) {
        var d = new Date(date);
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var year = d.getFullYear();

        return `${month}/${day}/${year}`;
    }

    function getDefaultParam() {
        var today = new Date();
        var transactionParam = {
            StartDateTime: convertDateFormat(new Date(today.setDate(today.getDate() - 30))),
            EndDateTime: convertDateFormat(new Date())
        };


        return transactionParam;
    }

    function getTransactionHistory(transactionParam) {
        vm.processing = true;

        transactionHistoryService.depositTransaction(transactionParam).then(function (result) {

            var depositTransactions = result.data.Payload;

            angular.forEach(depositTransactions, function (val, idx) {
                depositTransactions[idx].FormattedDate = ToFormattedDate(val.TransactionDate);
                depositTransactions[idx].Type = 'Deposit';
                depositTransactions[idx].TypeDictionary = dictionary.deposit;
                depositTransactions[idx].TransactionStatusStrDictionary = getStatusDictionary(val.TransactionStatusStr);
            });

            transactionHistoryService.withdrawTransaction(transactionParam).then(function (result) {

                var withdrawTransactions = result.data.Payload;

                angular.forEach(withdrawTransactions, function (val, idx) {
                    withdrawTransactions[idx].FormattedDate = ToFormattedDate(val.TransactionDate);
                    withdrawTransactions[idx].Type = 'Withdraw';
                    withdrawTransactions[idx].TypeDictionary = dictionary.withdraw;
                    withdrawTransactions[idx].TransactionStatusStrDictionary = getStatusDictionary(val.TransactionStatusStr);
                });


                var transactions = [];

                if (depositTransactions) {
                    transactions = depositTransactions.concat(withdrawTransactions);
                }
                else if (withdrawTransactions) {
                    transactions = withdrawTransactions.concat(depositTransactions);
                }

                if (vm.types.deposit && vm.types.withdraw) {
                    // do nothing
                    vm.filterCount = vm.filterCount + 2;
                }

                else if (vm.types.deposit) {
                    transactions = transactions.filter(x => x.Type === 'Deposit');
                    vm.filterCount = vm.filterCount + 1;
                }

                else if (vm.types.withdraw) {
                    transactions = transactions.filter(x => x.Type === 'Withdraw');
                    vm.filterCount = vm.filterCount + 1;
                }

                else {
                    transactions = [];
                }

                var tableController = document.querySelector('#tableHistory');
                var tableScope = angular.element(tableController).scope();
                tableScope.vm.transactions = transactions;

                $('.filter-hidden').removeClass('is-active');
                $.modal.close();
                vm.ready = true;
                vm.processing = false;

            });
        });
    }

    function init() {
        if (!transactionTableInitiliazed) {
            getTransactionHistory(getDefaultParam());
            transactionTableInitiliazed = true;
        }
    }

    init();
}

function getStatusDictionary(val) {
    switch (val) {
        case 'In Progress':
            return dictionary.inProgress;
        case 'Completed':
            return dictionary.completed;
        case 'Cancelled':
            return dictionary.cancelled;
        case 'Failed':
            return dictionary.failed;
        default:
            return '';
    }
}

function transactionFilterController(transactionHistoryService) {
    var vm = this;

    vm.processing = false;
    vm.filterCount = 0;
    vm.ready = false;

    vm.statuses = [
        {
            name: dictionary.inProgress,
            alias: 'In Progress',
            checked: false
        },
        {
            name: dictionary.completed,
            alias: 'Completed',
            checked: false
        },
        {
            name: dictionary.cancelled,
            alias: 'Cancelled',
            checked: false
        },
        {
            name: dictionary.failed,
            alias: 'Failed',
            checked: false
        }
    ];

    vm.resetFilters = function () {
        angular.forEach(vm.statuses, function (status, index) {
            status.checked = false;
        });

        $("#start-date").val('');
        $("#end-date").val('');
        $("#start-date--mob").val('');
        $("#end-date--mob").val('');
    };

    vm.applyFilters = function () {
        vm.filterCount = 0;

        var startDate;
        var endDate;

        if (vm.viewPort === 'desktop') {
            startDate = $("#start-date").val();
            endDate = $("#end-date").val();
        }

        else {
            startDate = $("#start-date--mob").val();
            endDate = $("#end-date--mob").val();
        }

        if (startDate && endDate) {
            var transactionParam = {
                StartDateTime: startDate,
                EndDateTime: endDate
            };
            vm.filterCount = 2;
            getTransactionHistory(transactionParam);
        }
        else {
            getTransactionHistory(getDefaultParam());
        }
    };

    /// convert date format from mm/dd/yy to dd/mm/yy
    function convertDateFormat(date) {
        var d = new Date(date);
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var year = d.getFullYear();

        return `${month}/${day}/${year}`;
    }

    function getDefaultParam() {
        var today = new Date();
        var transactionParam = {
            StartDateTime: convertDateFormat(new Date(today.setDate(today.getDate() - 30))),
            EndDateTime: convertDateFormat(new Date())
        };

        return transactionParam;
    }

    function getTransactionHistory(transactionParam) {
        vm.processing = true;

        if (window.transactionHistoryType) {
            transactionHistoryService[window.transactionHistoryType](transactionParam).then(function (result) {

                var transactions = result.data.Payload;

                angular.forEach(transactions, function (val, idx) {
                    transactions[idx].FormattedDate = ToFormattedDate(val.TransactionDate);
                    transactions[idx].TransactionStatusStrDictionary = getStatusDictionary(val.TransactionStatusStr);
                });

                var statusFilters = vm.statuses.filter(x => x.checked).map(x => x.alias);

                if (statusFilters.length > 0) {
                    vm.filterCount = vm.filterCount + statusFilters.length;
                    transactions = transactions.filter(x => statusFilters.includes(x.TransactionStatusStr));
                }

                var tableController = document.querySelector('#tableHistory');
                var tableScope = angular.element(tableController).scope();
                tableScope.vm.transactions = transactions;

                $('.filter-hidden').removeClass('is-active');
                $.modal.close();
                vm.ready = true;
                vm.processing = false;
            });
        }
    }

    function init() {
        if (!transactionTableInitiliazed) {
            getTransactionHistory(getDefaultParam());
            transactionTableInitiliazed = true;
        }
    }

    init();
}

function ToFormattedDate(date) {
    return (new Date(parseInt(date.replace('/Date(', '').replace(')/', '')))).toLocaleString();
}

function registrationController($scope) {
    var vm = this;
    vm.submit = function () {
        if ($scope.registrationForm.$valid) {
            $(function () {
                $.magnificPopup.open({
                    items: {
                        src: $('#verification-emeil--you'),
                        type: 'inline'
                    }
                });
            });
        }
    };
}

function closeTicketController() {
    var vm = this;
    vm.closeTicket = function () {
        $('#close-ticket').modal();
    };
}

function closeTicketConfirmController($window, ticketService, utilityService) {
    var vm = this;
    vm.hasError = false;
    vm.processing = false;
    vm.submit = function (ticketId) {
        vm.processing = true;

        var closeTicket = {
            ticketId: ticketId,
            closeMessage: dictionary.closeTicketMessage
        };

        ticketService.closeTicket(closeTicket, settings.tenantUid).then(function (result) {
            utilityService.alert(dictionary.ticketHasBeenClosed, settings.contactUsUrl);
        }, function () {
            vm.hasError = true;
        });
    };
}

function createTicketController($scope, ticketService) {
    var vm = this;
    vm.name = 'createTicket';
    vm.ticket = {
        title: null,
        description: null,
        attachment: null
    };

    vm.processing = false;
    vm.disableBtn = false;

    vm.submit = function () {

        if ($scope.createTicket.$valid) {
            vm.data = {};
            ticketService.createTicket(vm.ticket, settings.tenantUid).then(
                function (result) {
                    vm.data = result.data;
                    if (!vm.data.Error) {
                        location.href = `${settings.viewTicketUrl}?id=${result.data.Result.Id}`;
                    }
                    vm.processing = false;
                }
            );

            vm.processing = true;
            vm.disableBtn = true;
        }
    };
}

function createTicketAnonymousController($scope, $timeout, ticketService) {
    var vm = this;
    vm.name = 'createTicketAnonymous';
    vm.processing = false;
    vm.disableBtn = false;
    vm.ticketAnonymous = {
        title: null,
        description: null,
        attachment: null,
        emailAddress: null,
        phoneNumber: null,
        name: null,
        surname: null
    };

    vm.submit = function () {
        if ($scope.createTicketAnonymous.$valid) {
            vm.data = {};
            vm.processing = true;
            vm.disableBtn = true;
            ticketService.createTicketAnonymous(vm.ticketAnonymous, settings.tenantUid).then(function (result) {
                vm.data = result.data;
                if (!vm.data.Error) {
                    vm.success = dictionary.ticketSentToAdmin;
                    $timeout(function () {
                        $.modal.close();
                        vm.data = {};
                        vm.ticketAnonymous = {};
                        $scope.createTicketAnonymous.$setPristine();
                        vm.success = '';
                    }, 3000);
                }
                vm.processing = false;
            }, function () {
                vm.processing = false;
                vm.disableBtn = false;
            });
        }
    };
}

function createMessageController(ticketService, $scope, $window) {
    var vm = this;
    vm.name = 'createMessage';
    vm.processing = false;
    vm.message = {
        ticketId: 0,
        emailAddress: null,
        messageText: null,
        attachment: null
    };

    vm.submit = function () {
        if ($scope.createMessage.$valid) {
            vm.processing = true;
            ticketService.createMessage(vm.message, settings.tenantUid)
                .then(function (result) {
                    vm.data = result.data;
                    if (!vm.data.Error) {
                        vm.processing = false;
                        $window.location.href = `${settings.viewTicketUrl}?id=${vm.message.ticketId}`;
                    }
                    vm.processing = false;
                });

        }
    };
}

function ticketController($window, fileService, ticketService, Upload) {
    var vm = this;
    vm.processing = false;
    vm.showReplyBox = false;
    vm.replyForm = {
        ticketId: 0,
        emailAddress: null,
        messageText: null,
        attachment: null
    };

    vm.submit = function () {
        vm.processing = true;
        ticketService.createMessage(vm.replyForm, settings.tenantUid)
            .then(function (result) {
                vm.data = result.data;
                if (!vm.data.Error) {
                    $window.location.reload();
                }
                vm.processing = false;
            });
    };

    vm.items = [];
    vm.upload = function (file) {
        if (file) {
            var item = {
                progressPercentage: 0,
                name: file.name
            };

            var length = vm.items.push(item);
            fileService.upload(file, settings.tenantUid)
                .then(function (result) {
                    item.path = result.data.FileUrl;

                    vm.replyForm.attachment = vm.items.map(item => item.path).join(', ');
                }, function () {
                    //console.log('Error uploading item');

                }, function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                    vm.items[length - 1].progressPercentage = progressPercentage;
                });
        }
    };

    vm.remove = function (item) {
        var index = vm.items.indexOf(item);
        vm.items.splice(index, 1);
        vm.replyForm.attachment = vm.items.map(item => item.path).join(', ');
    };
}

function fileController($scope, fileService, Upload) {
    var vm = this;

    vm.items = [];
    vm.upload = function (file) {
        if (file) {
            var item = {
                progressPercentage: 0,
                name: file.name
            };
            if (vm.items.length > 0) return;
            var length = vm.items.push(item);

            var uploadMode;
            if ($scope.$parent.vm.name === 'createTicket') {
                uploadMode = 'upload';
            }
            else {
                uploadMode = 'uploadAnonymous';
            }

            fileService[uploadMode](file, settings.tenantUid)
                .then(function (result) {
                    item.path = result.data.FileUrl;
                    updateParentAttachment();

                }, function () {
                    //console.log('Error uploading item');

                }, function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                    vm.items[length - 1].progressPercentage = progressPercentage;
                });
        }
    };

    vm.remove = function (item) {
        var index = vm.items.indexOf(item);
        vm.items.splice(index, 1);
        updateParentAttachment();
    };

    function updateParentAttachment() {
        var items = vm.items.map(item => item.path).join(', ');
        switch ($scope.$parent.vm.name) {
            case 'createTicket':
                $scope.$parent.vm.ticket.attachment = items;
                break;
            case 'createTicketAnonymous':
                $scope.$parent.vm.ticketAnonymous.attachment = items;
                break;
        }
    }
}

function addCardController($scope, $window, cardService, utilityService) {
    var vm = this;
    vm.processing = false;
    vm.card = {
        cardNumber: '',
        iban: ''
    };

    vm.addCard = function () {
        if ($scope.addCard.$valid) {
            vm.processing = true;
            cardService.addCard(vm.card).then(function (result) {
                vm.data = result.data;
                //console.log(vm.data);
                if (!vm.data.Errors && vm.data.Success) {
                    utilityService.alert(dictionary.paymentMethodAdded);
                }

                else {
                    if (!vm.data.Message) {
                        vm.data.Errors = {
                            ErrorMessage: 'An unknown error has occured'
                        };
                    }
                    vm.processing = false;
                }
            });
        }
    };

    vm.cancel = function () {
        $.modal.close();
    };
}

function addCardWithdrawController($scope, $window, cardService, utilityService) {
    var vm = this;
    vm.processing = false;
    vm.newCard = false;
    vm.card = {
        cardNumber: '',
        iban: ''
    };

    vm.allCards = [];

    vm.getCards = function () {
        cardService.activeCardsAll().then(function (result) {
            vm.allCards = result.data.Payload.ActiveCards;
            vm.card.cardNumber = vm.allCards[0].CardNumber;
            vm.card.iban = vm.allCards[0].Iban;
            if (vm.card.iban == '') {
                vm.cardHasIban = false;
            }
            else {
                vm.cardHasIban = true;
            }
        });
    };

    vm.selectCard = function (cardNum) {
        var test = vm.allCards.find(x => x.CardNumber == cardNum);
        if (test != null) {
            vm.newCard = false;
            vm.card.iban = test.Iban;
            if (vm.card.iban == '') {
                vm.cardHasIban = false;
            }
            else {
                vm.cardHasIban = true;
            }
        }
        else {
            vm.newCard = true;
            vm.cardHasIban = false;
            vm.card.iban = '';
        }
     };

    vm.addCard = function () {
        if ($scope.addCard.$valid) {
            vm.processing = true;
            if (vm.allCards == '' || vm.newCard) {
                cardService.addCard(vm.card).then(function (result) {
                    vm.data = result.data;
                    console.log(vm.data);
                    if (!vm.data.Errors && vm.data.Success) {
                        utilityService.alert(dictionary.paymentMethodAdded);
                    }

                    else {
                        if (!vm.data.Message) {
                            vm.data.Errors = {
                                ErrorMessage: 'An unknown error has occured'
                            };
                        }
                        vm.processing = false;
                    }
                });
            }
            else {
                cardService.addIban(vm.card).then(function (result) {
                    vm.data = result.data;
                    console.log(vm.data);
                    if (!vm.data.Errors && vm.data.Success) {
                        utilityService.alert(dictionary.paymentMethodAdded);
                    }

                    else {
                        if (!vm.data.Message) {
                            vm.data.Errors = {
                                ErrorMessage: 'An unknown error has occured'
                            };
                        }
                        vm.processing = false;
                    }
                });
            };
        }
    };

    vm.cancel = function () {
        $.modal.close();
    };
}

function addIbanController($scope, $window, $timeout, cardService, utilityService) {
    var vm = this;
    vm.processing = false;
    vm.card = {
        iban: ''
    };

    vm.submit = function () {
        vm.processing = false;
        vm.data = {};
        if ($scope.addIban.$valid) {
            vm.processing = true;
            cardService.addIban(vm.card).then(function (result) {
                vm.data = result.data;
                if (!vm.data.Errors) {
                    //console.log(result);
                    utilityService.alert(dictionary.paymentMethodUpdated);
                }
                vm.processing = false;
            });
        }
    };

    vm.cancel = function () {
        $.modal.close();
    };
}

function editCardController($scope, $window, $timeout, cardService, utilityService) {
    var vm = this;
    vm.processing = false;
    vm.card = {
        cardNumber: '',
        iban: ''
    };

    vm.submit = function () {
        vm.processing = false;
        vm.data = {};
        if ($scope.editCard.$valid) {
            vm.processing = true;
            cardService.updateCard(vm.card).then(function (result) {
                vm.data = result.data;
                if (!vm.data.Errors) {
                    //console.log(result);
                    utilityService.alert(dictionary.paymentMethodUpdated);
                }
                vm.processing = false;
            });
        }
    };

    vm.cancel = function () {
        $.modal.close();
    };
}

function deleteCardConfirmController($window, cardService, utilityService) {
    var vm = this;
    vm.hasError = false;
    vm.processing = false;
    vm.submit = function (cardNumber) {
        vm.processing = true;
        cardService.deleteCard(cardNumber, settings.tenantUid).then(function (result) {
            utilityService.alert(dictionary.paymentMethodDeleted);
        }, function () {
            vm.hasError = true;
        });
    };

    vm.cancel = function () {
        $.modal.close();
    };
}

function transactionController(
    transactionService,
    tenantService,
    utilityService,
    cardService,
    $timeout,
    $window,
    $scope) {

    'use strict';

    var vm = this;
    vm.processing = false;
    vm.ready = false;
    vm.showPaymentList = false;
    vm.amount = '';
    vm.submitText = dictionary.depositAdd;
    vm.cardNumberLabel = dictionary.cardNumber16Digits;
    vm.amountLabel = dictionary.amount;
    vm.cardNameLabel = dictionary.cardName;
    vm.cardCvv = dictionary.cardCVV;


    vm.predefinedAmounts = TRANSACTION.predefinedAmounts;

    vm.inputTypes = {
        creditCard: {
            creditCard: true
        },
        amount: {
            numeral: true,
            numeralThousandsGroupStyle: 'none'
        },
        number: {
            numeral: true
        }
    };

    $scope.$watch('vm.tenantCard.selectedCard', function () {
        var agree = document.getElementById("agreeToTerms");
        if (agree != null) {
            agree.checked = false;
        }
        if (vm.tenantCard && vm.tenantCard.selectedCard) {
            if (settings.customerCurrency !== vm.tenantCard.selectedCard.PaymentSystemCurrency) {
                utilityService.getCurrency(vm.tenantCard.selectedCard.PaymentSystemCurrency, settings.customerCurrency, vm.tenantCard.selectedCard.PaymentSystemNameOrig).then(function (result) {
                    vm.currencyInfo = `(${result.data.Value} ${settings.customerCurrency} = 1 ${vm.tenantCard.selectedCard.PaymentSystemCurrency})`;
                    if (result.data.Value == 0.1) {
                        vm.currencyInfo = `(${result.data.Value*10} ${settings.customerCurrency} = 10 ${vm.tenantCard.selectedCard.PaymentSystemCurrency})`;
                    }
                });
            }
            var transactionFields = [];
            angular.forEach(vm.tenantCard.selectedCard[TRANSACTION.fields], function (val, idx) {
                if (val.Type === "amount" && vm.predefinedAmounts) {
                    transactionFields.push({ Type: 'predefinedAmounts' });
                }
                transactionFields.push(val);
            });
            vm.transactionFields = transactionFields;
        }

        vm.data = null;
        vm.processing = false;
        $scope.transactForm.$setPristine();
    });

    vm.submitText = TRANSACTION.submitText;

    vm.tenantCard = {
        cards: [],
        selectedCard: null,
        selectTenantCard: function (card) {
            vm.no_cards = false;
            this.selectedCard = card;

            var test = card[TRANSACTION.fields].find(x => x.Type === 'card');
            if (test && vm.customerCard.cards.length == 0) {
                vm.no_cards = true;
            }
            if (vm.tenantCard[TRANSACTION.fields]) {
                vm.amount = vm.tenantCard.selectedCard[TRANSACTION.fields].find(x => x.Type === 'amount').Value;
            }
        }
    };

    vm.updateAmount = function (amount) {
        vm.tenantCard.selectedCard[TRANSACTION.fields].find(x => x.Type === 'amount').Value = amount;
        vm.amount = amount;
    };

    vm.autoThousands = function (amount, paymentId) {
        if (amount % 1000 !== 0 && paymentId === 'CartiPal') {
            amount -= amount % 1000;
            vm.tenantCard.selectedCard[TRANSACTION.fields].find(x => x.Type === 'amount').Value = amount;
            vm.amount = amount;
        }
    }

    vm.transact = function () {

        vm.data = {};
        vm.errorCardRequired = null;
        vm.processing = true;
        vm.hideForm = false;

        if (vm.agreeTerms) {

            var transactParam = {
                paymentIdentifier: vm.tenantCard.selectedCard.PaymentIdentifier,
                paymentSystemName: vm.tenantCard.selectedCard.PaymentSystemName,
                parameters: {}
            };

            if (vm.tenantCard.selectedCard[TRANSACTION.fields]) {
                angular.forEach(vm.tenantCard.selectedCard[TRANSACTION.fields], function (val, idx) {
                    transactParam.parameters[val.Name] = val.Value;
                });
            }

            transactParam.redirectUrl = settings.depositUrlAbsolute;

            transactionService[TRANSACTION.type](transactParam).then(function (result) {
                vm.data = result.data;
                if (!vm.data.Errors) {
                    if (vm.data.Payload) {
                        if (TRANSACTION.type === 'deposit') {
                            if (vm.data.Payload.RedirectType === null) {
                                $window.location.href = vm.data.Payload.PaymentUrl;
                            }
                            if (vm.data.Payload.RedirectType === '1') {
                                PerfectMoneyPost(vm.data.Payload.PaymentUrl, vm.data.Payload.Parameters);
                            }
                            if (vm.data.Payload.RedirectType === '2') {
                                vm.bitCoinDetails = vm.data.Payload;
                                vm.processing = false;
                                vm.hideForm = true;
                            }
                        }

                        if (TRANSACTION.type === 'withdraw') {
                            $window.location.href = settings.withdrawUrl + '?isConfirmed=true';
                        }
                    }
                }
                else {
                    if (vm.data.Errors.ErrorCode == 168) {
                        sendVerifEmail();
                    }
                    if (vm.data.Errors.ErrorMessage) {
                        if (vm.data.Errors.ErrorMessage === dictionary.withdrawBelowMinimum) {
                            vm.data.Errors.ErrorMessage = dictionary.belowMinimumWithdrawalAmount;
                        }
                        if (vm.data.Errors.ErrorMessage === dictionary.withdrawInsufficientBalance) {
                            vm.data.Errors.ErrorMessage = dictionary.insufficientBalance;
                        }
                    }

                    vm.processing = false;
                }
            });
        }
    };

    vm.customerCard = {
        cards: null,
        activeCard: null,
        selectCard: function (card) {
            this.activeCard = card;
        }
    };

    function init() {
        tenantService.getCards(settings.rootGuid, settings.customerGuid, settings.customerToken, settings.pageId).then(function (result) {
            if (result.data.length != 0) {
                vm.tenantCard.cards = result.data;
                for(var x = 0; x < vm.tenantCard.cards.length; x++){
                    if (vm.tenantCard.cards[x].isDefault) {
                        vm.tenantCard.selectedCard = vm.tenantCard.cards[x];
                    }
                }

                cardService.activeCards().then(function (result) {
                    var test = vm.tenantCard.selectedCard[TRANSACTION.fields].find(x => x.Type === 'card');
                    if (test && result.data.Payload.ActiveCards.length == 0) {
                        vm.no_cards = true;
                    }

                    vm.customerCard.cards = result.data.Payload.ActiveCards;
                    vm.customerCard.activeCard = vm.customerCard.cards[0];

                    var cardField = vm.tenantCard.selectedCard[TRANSACTION.fields].find(x => x.Type === 'card');
                    if (cardField) {
                        if (vm.customerCard.activeCard) {
                            cardField.Value = vm.customerCard.activeCard.CardNumber;
                        }
                    }
                    vm.ready = true;

                });
            }
            else {
                vm.hideForm = true;
                vm.noGateway = true;
                vm.ready = true;
            }

        });
    }

    init();

    function PerfectMoneyPost(path, params, method = 'post') {

        const form = document.createElement('form');
        form.method = method;
        form.action = path;

        for (const key in params) {
            if (params.hasOwnProperty(key)) {
                const hiddenField = document.createElement('input');
                hiddenField.type = 'hidden';
                hiddenField.name = key;
                hiddenField.value = params[key];

                form.appendChild(hiddenField);
            }
        }
        document.body.appendChild(form);
        form.submit();
    }

}

function GameGridController(gameService) {
    var vm = this;

    vm.tenantUid = '';
    vm.category = '';
    vm.subcategory = '';
    vm.provider = '';
    vm.keyword = '';
    vm.agreeTerms = false;
    vm.languageCode = '';
    vm.gameGrid = null;
    vm.hasError = false;
    vm.filter = '';
    vm.subcategories = [];
    vm.providers = [];
    vm.isLoggedIn = false;
    vm.selectedGame = null;
    vm.isMobile = false;
    vm.gameId = null;
    var data = {};

    vm.isLoggedIn = Cookies.get('username') !== undefined ? true : false;
    if (openPopup) {
        vm.gameId = parseInt(getUrlParameter("gameId"));
    }
    if (isMobile) {
        vm.isMobile = isMobile;
    }

    vm.init = function (category, tenantUid, languageCode) {
        vm.tenantUid = tenantUid;
        vm.category = category;
        vm.languageCode = languageCode;
        data = {
            'tenantUid': vm.tenantUid,
            'category': vm.category,
            'subcategory': vm.subcategory,
            'provider': vm.provider,
            'keyword': vm.keyword,
            'languageCode': vm.languageCode
        };
        vm.getGameGrid(data);
    };

    vm.getGameGrid = function (data) {
        gameService.getGameGrid(data).then(function (result) {
            if (result.data !== null && result.data !== "") {
                vm.gameGrid = result.data;
                if (vm.gameId !== null) {
                    vm.selectedGame = vm.gameGrid.filter(function (element) {
                        return element.GameId === vm.gameId;
                    });
                    if (vm.selectedGame[0].DemoEnabled) {
                        $('#game-mode-selection').modal();
                    }
                }
                vm.getSubCategories();
                vm.getProviders();
            }
            else {
                //console.log("No games returned");
            }
        }, function (e) {
            vm.hasError = true;
            //console.log(e);
        });
    };

    vm.updateGrid = function (filter) {
        if (filter === 'new') {
            vm.filter = { 'NewGame': true };
        }
        else if (filter === 'favourite') {
            vm.filter = { 'Favourite': true };
        }
        else {
            vm.filter = filter;
        }

        $('.grid').css('display', 'none');
        setTimeout(function () {
            $('.grid').css('display', '');
        }, 1);
    };

    vm.selectGame = function (game) {
        vm.selectedGame = [game];
    };

    vm.openGame = function (e) {

        if (!vm.agreeTerms) {
            e.preventDefault();
        }
    };

    vm.getFullImage = function (array) {
        for (var i = 0; i < array.length; i++) {
            if (array[i].ImageType === 2) {
                if (array[i].Url !== null) {
                    return array[i];
                }
                else {
                    return { "Url": "https://via.placeholder.com/150" };
                }
            }
        }
        return null;
    };

    vm.getThumbnailImage = function (array) {
        for (var i = 0; i < array.length; i++) {
            if (array[i].ImageType === 1) {
                return array[i];
            }
        }
        return null;
    };

    vm.getThumbnailImageL = function (array) {
        for (var i = 0; i < array.length; i++) {
            if (array[i].ImageType === 2) {
                //console.log("big thumb loaded")
                return array[i];
            }
        }
        return null;
    };

    vm.getSubCategories = function () {
        vm.subcategories = new Array();
        var counter = 0;
        vm.gameGrid.forEach(function (value) {
            if (value.SubCategory !== null && value.SubCategory !== "" && !exists(value.SubCategory, vm.subcategories)) {
                vm.subcategories[counter] = value.SubCategory;
                counter++;
            }
        });
    };

    vm.checkNew = function () {
        return angular.equals(vm.filter, { NewGame: true });
    };

    vm.checkFavourite = function () {
        return angular.equals(vm.filter, { Favourite: true });
    };

    vm.getProviders = function () {
        vm.providers = new Array();
        var counter = 0;
        vm.gameGrid.forEach(function (value) {
            if (value.GameProvider !== null && value.GameProvider !== "" && !exists(value.GameProvider, vm.providers)) {
                vm.providers[counter] = value.GameProvider;
                counter++;
            }
        });
    };

    vm.removeSpaces = function (text) {
        return text.replace(" ", "");
    };

    vm.searchGame = function () {
        data = {
            'tenantUid': vm.tenantUid,
            'category': vm.category,
            'subcategory': vm.subcategory,
            'provider': vm.provider,
            'keyword': vm.keyword,
            'languageCode': vm.languageCode
        };
        gameService.getGameGrid(data).then(function (result) {
            vm.gameGrid = result.data;
            vm.getSubCategories();
            vm.getProviders();
        }, function (e) {
            vm.hasError = true;
            //console.log(e);
        });
    };

    function exists(value, elements) {
        return elements.includes(value);
    }

}

function mobileMenuController() {
    var vm = this;
    vm.tabs = {
        tab1: {
            active: true
        },
        tab2: {
            active: false
        },
        tab3: {
            active: false
        }
    };

    vm.activateTab = function (tab) {
        angular.forEach(vm.tabs, (val) => val.active = false);
        tab.active = true;
    };
}


function allBonusTransactionHistoryTableController() {
    var vm = this;
    vm.bonustransactions = [];
}


var bonusTransactionTableInitiliazed = false;
function allBonusTransactionHistoryFilterController(transactionHistoryService) {
    var vm = this;

    vm.processing = false;
    vm.ready = false;

    vm.resetFilters = function () {
        $("#start-date").val('');
        $("#end-date").val('');
        $("#start-date--mob2").val('');
        $("#end-date--mob2").val('');
    };

    vm.applyFilters = function () {

        var startDate;
        var endDate;

        if (vm.viewPort === 'desktop') {
            startDate = $("#start-date").val();
            endDate = $("#end-date").val();
        }
        else {
            startDate = $("#start-date--mob2").val();
            endDate = $("#end-date--mob2").val();
        }

        if (startDate && endDate) {
            var transactionParam = {
                StartDate: startDate + " 00:00:00",
                EndDate: endDate + " 23:59:59"
            };
            getTransactionHistory(transactionParam);
        }
        else {
            getTransactionHistory(getDefaultParam());
        }
    };


    /// convert date format from mm/dd/yy to dd/mm/yy
    function convertDateFormat(date) {
        var d = new Date(date);
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var year = d.getFullYear();

        return `${month}/${day}/${year}`;
    }

    function getDefaultParam() {
        var today = new Date();
        var transactionParam = {
            StartDate: convertDateFormat(new Date(today.setDate(today.getDate() - 30))) + " 00:00:00",
            EndDate: convertDateFormat(new Date()) + " 23:59:59"
        };
        return transactionParam;
    }

    function getTransactionHistory(transactionParam) {
        vm.processing = true;

        transactionHistoryService.bonusTransaction(transactionParam).then(function (result) {
            var resultTransaction = result.data.Payload;

            angular.forEach(resultTransaction, function (val, idx) {
                resultTransaction[idx].FormattedDate = ToFormattedDate(val.BonusApplied);
                resultTransaction[idx].TypeDictionary = dictionary.depositbonus;
                resultTransaction[idx].StatusStrDictionary = getBonusStatusDictionary(val.StatusStr);
                resultTransaction[idx].RedemptionStatusStrDictionary = getBonusRedemptionStatusDictionary(val.RedemptionStatusStr);

                angular.forEach(resultTransaction[idx].RedemptionData, function (val2, idx2) {
                    resultTransaction[idx].RedemptionData[idx2].Name = getBonusRedemptionDataName(val2.Name);
                });
            });

            var tableController = document.querySelector('#tableHistory');
            var tableScope = angular.element(tableController).scope();
            tableScope.vm.bonustransactions = resultTransaction;

            $('.filter-hidden').removeClass('is-active');
            $.modal.close();
            vm.ready = true;
            vm.processing = false;

        });
    }

    function init() {
        if (!bonusTransactionTableInitiliazed) {
            getTransactionHistory(getDefaultParam());
            bonusTransactionTableInitiliazed = true;
        }
    }

    init();

}

function getBonusStatusDictionary(val) {
    switch (val) {
        case 'Active':
            return dictionary.active;
        case 'Closed':
            return dictionary.closed;
        default:
            return '';
    }
}

function getBonusRedemptionStatusDictionary(val) {
    switch (val) {
        case 'Completed':
            return dictionary.completed;
        case 'Incomplete':
            return dictionary.incomplete;
        default:
            return '';
    }
}

function getBonusRedemptionDataName(val) {
    switch (val) {
        case 'TotalDepositAmount':
            return dictionary.totalDepositAmount;
        case 'TotalBetAmount':
            return dictionary.totalBetAmount;
        case 'TotalSportsbookAmount':
            return dictionary.totalSportsbookAmount;
        case 'TotalCasinoAmount':
            return dictionary.totalCasinoAmount;
        case 'TotalPokerAmount':
            return dictionary.totalPokerAmount;
        case 'TotalLiveCasinoAmount':
            return dictionary.totalLiveCasinoAmount;
        case 'TotalLotteryAmount':
            return dictionary.totalLotteryAmount;
        default:
            return '';
    }
}