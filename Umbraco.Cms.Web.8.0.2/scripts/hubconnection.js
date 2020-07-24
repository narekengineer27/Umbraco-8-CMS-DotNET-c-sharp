"use strict";

if (Cookies.get('username') !== undefined) {

    var hub = new signalR.HubConnectionBuilder()
    .withUrl(`${settings.balanceApi}/GetHubConnectionDetails`, { accessTokenFactory: () => settings.customerToken })
    .withAutomaticReconnect()
        .build();

    hub.on('BalanceUpdate', function (response) {
        if (response) {
            console.log('Balance Update response', response);
        }
    });

    hub.start().catch(error => this.onConnectionError(error)).then(() => {
        console.log('hub started');
    });
}

function onConnectionError(error) {
    console.log(error);
}