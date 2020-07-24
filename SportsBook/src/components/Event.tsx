import React from 'react';
import { IEventNormalized } from '../lib/SportsBookModel/interfaces/IEvent';
import { IMarket } from '../lib/SportsBookModel/interfaces/IMarket';
import { List } from 'semantic-ui-react';
import { IOutcome } from '../lib/SportsBookModel/interfaces/IOutcome';

interface IEventPropTypes {
  event: IEventNormalized;
  onSelect: any;
  isActive: boolean;
}

interface IEventStateTypes {
  odds: any;
}

class Event extends React.Component<IEventPropTypes> {
  state: IEventStateTypes = {
    odds: [],
  };

  // constructor(props: IEventPropTypes) {
  //   super(props);
  // }

  makeOutcomes(outcomes: IOutcome[]): any {
    return <List.List as='ul'>
      {outcomes.map((outcome, index) => {
        return (
          <List.Item as='li' key={index}>
            {outcome.name}: {outcome.odds}
          </List.Item>
        );
      })}
    </List.List>;
  }

  makeOdds(odds: IMarket[]): any {
    return <List.List as='ul'>
      {odds.map((odd, index) => {
        return (
          <List.Item as='li' key={index}>
            {odd.name}: {this.makeOutcomes(odd.outcomes)}
          </List.Item>
        );
      })}
    </List.List>;
  }

  componentDidMount(): void {
    this.setState({ odds: this.makeOdds(this.props.event.topMarketLines) });
  }

  componentWillUpdate(nextProps: Readonly<IEventPropTypes>, nextState: Readonly<{}>, nextContext: any): void {
    // this.setState({odds: this.makeOdds(nextProps.event.topMarketLines)});
  }

  render() {
    return (
      <List.List as='ul'>
        <List.Item as='li' key='1'>
          {this.props.event.name}
        </List.Item>

        <List.Item as='li' key='2'>
          {this.state.odds}
        </List.Item>
      </List.List>
    );
  }
}

export default Event;
