import * as signalR from '@aspnet/signalr';
import axios from 'axios';
import API_URLS_CONFIG, { EVENT_NAMES } from './config';

/*

  this.hub = new signalR.HubConnectionBuilder()
    .withUrl(details.url, { accessTokenFactory: () => details.token })
    .build();

  this.hub.start().catch(err => console.error(err)).then(() => {
    console.log('hub started');
  });

  this.hub.on('PlaceBet', (response: any) => {
    if (response) {
      console.log('Place bet response', response);
  //this.store.dispatch(new actions.BetResponseReceived(response));
    }
  });

  this.hub.on('EventSnapshot', (event: SportEvent) => {
    console.log(Update for event ${event.id});
  //this.store.dispatch(new fixtureActions.EventUpdateReceived(event));
  });

*/

// Then you need to send a post to SubscribeToEventMonitoring with { userId: customerGuid } -
// if you want to get the market updates before the customer is logged in,
// just generate a random userId and then re-connect when they eventually log in - let me know if you have any problems

export function SubscribeToEventMonitoring(token: string, url: string, callback: (data: any) => any, onError: (error: any) => any) {
  console.log('Event monitoring token is', token);

  const connection = new signalR.HubConnectionBuilder()
    .withUrl(url, {
      accessTokenFactory: () => token,
    })
    .build();

  connection.start().catch(onError);

  connection.on(EVENT_NAMES.EVENTS_UPDATE, callback);

  return connection;
}

export function GetUpcomingEvents(callback: (data: any) => any, onError: (error: any) => any) {
  axios.get(API_URLS_CONFIG.GET_UPCOMING_EVENTS)
    .then(response => callback(response.data))
    .catch(onError);
}

export function GetHubConnectionDetails(callback: (token: string, url: string) => any, onError: (error: any) => any) {
  axios.post(API_URLS_CONFIG.GET_HUB_CONNECTION_DETAILS, { userId: 'test-customer-000003' })
    .then(response => callback(response.data.token, response.data.url))
    .catch(onError);
}

export default GetHubConnectionDetails;
