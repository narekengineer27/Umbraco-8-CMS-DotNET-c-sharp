import React from 'react';
import { Accordion, Icon } from 'semantic-ui-react'
import { ITournamentNormalized } from '../lib/SportsBookModel/interfaces/ITournament';
import { IEventNormalized } from '../lib/SportsBookModel/interfaces/IEvent';
import { ICategoryNormalized } from '../lib/SportsBookModel/interfaces/ICategory';
import Tournament from './Tournament';

interface ICategoryPropTypes {
  category: ICategoryNormalized;
  events: IEventNormalized[];
  tournaments: ITournamentNormalized[];
  onSelect: any;
  isActive: boolean;
}

interface ICategoryState {
  selectedTournament: number;
}

class Category extends React.Component<ICategoryPropTypes> {
  tournaments: any;

  state: ICategoryState = {
    selectedTournament: -1,
  };

  componentWillUpdate(nextProps: any, nextState: ICategoryState) {
    this.tournaments = this.props.tournaments.map((tournament, index) => {
      const events = this.props.events.filter(event => event.tournamentId === tournament.id);

      return <Tournament key={tournament.id} tournament={tournament} events={events}
                         onSelect={this.selectTournament(index)} isActive={nextState.selectedTournament === index}/>
    });
  }

  selectTournament(id: number): any {
    return () => {
      this.setState((state: ICategoryState) => ({
        selectedTournament: (state.selectedTournament === id) ? -1 : id
      }));
    };
  }

  render() {
    return (
      <Accordion fluid styled>
        <Accordion.Title active={this.props.isActive} onClick={this.props.onSelect}>
          <Icon name='dropdown'/>
          {this.props.category.name} ({this.props.events.length})
        </Accordion.Title>
        <Accordion.Content active={this.props.isActive}>
          {this.tournaments}
        </Accordion.Content>
      </Accordion>
    );
  }
}

export default Category;
