import React from 'react';
import { Accordion, Icon } from 'semantic-ui-react'
import { ITournamentNormalized } from '../lib/SportsBookModel/interfaces/ITournament';
import { IEventNormalized } from '../lib/SportsBookModel/interfaces/IEvent';
import Event from './Event';

interface ITournamentPropTypes {
  tournament: ITournamentNormalized;
  events: IEventNormalized[];
  onSelect: any;
  isActive: boolean;
}

interface ITournamentState {
  selectedEvent: any;
}

class Tournament extends React.Component<ITournamentPropTypes> {
  events: any;

  state: ITournamentState = {
    selectedEvent: -1,
  };

  componentWillUpdate(nextProps: any, nextState: ITournamentState) {
    this.events = this.props.events.map((event, index) => {
      return <Event key={event.id} event={event} onSelect={this.selectEvent(index)}
                    isActive={nextState.selectedEvent === index}/>
    });

  }

  selectEvent(id: number): any {
    return () => {
      this.setState((state: ITournamentState) => ({
        selectedEvent: (state.selectedEvent === id) ? -1 : id
      }));
    };
  }

  render() {
    return (
      <Accordion fluid styled>
        <Accordion.Title active={this.props.isActive} onClick={this.props.onSelect}>
          <Icon name='dropdown'/>
          {this.props.tournament.name} ({this.props.events.length})
        </Accordion.Title>
        <Accordion.Content active={this.props.isActive}>
          {this.events}
        </Accordion.Content>
      </Accordion>
    );
  }
}

export default Tournament;
