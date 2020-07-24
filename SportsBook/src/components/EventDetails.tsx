import React from 'react';
import { IEventNormalized } from '../lib/SportsBookModel/interfaces/IEvent';
import { Container, List } from 'semantic-ui-react';
import * as moment from 'moment';
import { IMarketGroup } from '../lib/SportsBookModel/interfaces/IMarketGroup';
import { IMarket } from '../lib/SportsBookModel/interfaces/IMarket';
import { IOutcome } from '../lib/SportsBookModel/interfaces/IOutcome';

interface IEventPropTypes {
  event: IEventNormalized;
}

interface IEventStateTypes {
  markets: any;
}

function formatDate(d: string): string {
  // @ts-ignore
  return moment(d).format('dddd, MMMM Do YYYY, h:mm:ss a');
}

class EventDetails extends React.Component<IEventPropTypes> {
  markets: any;

  state: IEventStateTypes = {
    markets: ''
  };

  constructor(props: IEventPropTypes) {
    super(props);

    this.markets = this.makeMarketGroups(props.event.marketGroups);
  }

  makeOutcomes(outcomes: IOutcome[]): any {
    let resOutcomes = [];

    for (let i in outcomes) {
      resOutcomes.push(<List.Item>
        <List.List>
          <List.Item>Name: {outcomes[i].name}</List.Item>
          <List.Item>Active: {outcomes[i].active}</List.Item>
          <List.Item>Dead heat factor: {outcomes[i].deadHeatFactor}</List.Item>
          <List.Item>Odds: {outcomes[i].odds}</List.Item>
          <List.Item>Probabilities: {outcomes[i].probabilities}</List.Item>
          <List.Item>Void factor: {outcomes[i].voidFactor}</List.Item>
        </List.List>
      </List.Item>);
    }

    return <List.List>{resOutcomes}</List.List>;
  }

  makeMarkets(markets: IMarket[]): any {
    let resMarkets = [];

    for (let i in markets) {
      resMarkets.push(<List.Item key={i}>
        {markets[i].name}
        {this.makeOutcomes(markets[i].outcomes)}
      </List.Item>)
    }

    return <List.List>{resMarkets}</List.List>;
  }

  makeMarketGroups(groups: IMarketGroup[]): any {
    // console.log('Make market groups', groups);
    let resGroups = [];

    for (let i in groups) {
      // console.log('markets count', groups[i].markets);

      if (!groups[i].markets.length) {
        continue;
      }

      resGroups.push(<List.Item key={i}>
        {groups[i].name}
        {this.makeMarkets(groups[i].markets)}
      </List.Item>);
    }

    return <List.List>{resGroups}</List.List>;
  }

  componentDidMount(): void {
    // console.log('Component did mount', this.props.event);
    // this.markets = this.makeMarketGroups(this.props.event.marketGroups);
    this.markets = this.makeMarkets(this.props.event.topMarketLines);
    // console.log('This markets:', this.markets);
  }

  componentWillUpdate(nextProps: any, nextState: IEventStateTypes) {
    // console.log('Component will update', nextProps, nextState);
    this.markets = this.makeMarkets(nextProps.event.topMarketLines);
    // this.markets = this.makeMarketGroups(nextProps.event.marketGroups);
    // console.log('This markets:', this.markets);
  }

  render() {
    return (
      <Container>
        <List as='ul'>
          <List.Item as='li'>Scheduled start time: {formatDate(this.props.event.scheduledStartTime)}</List.Item>
          {this.props.event.scheduledEndTime ?
            <List.Item as='li'>Scheduled end time: {formatDate(this.props.event.scheduledEndTime)}}</List.Item> : ''}
          <List.Item as='li'>Status description: {this.props.event.statusDescription}</List.Item>
          <List.Item as='li'>
            Odds
            {this.markets}
          </List.Item>
        </List>
      </Container>
    );
  }
}

export default EventDetails;
