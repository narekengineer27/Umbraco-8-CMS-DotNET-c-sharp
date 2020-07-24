import React, { CSSProperties, MouseEvent } from 'react';
import { Accordion, Icon } from 'semantic-ui-react'
import { ISport } from '../lib/SportsBookModel/interfaces/ISport';
import { IEventNormalized } from '../lib/SportsBookModel/interfaces/IEvent';
import { ICategoryNormalized } from '../lib/SportsBookModel/interfaces/ICategory';
import { ITournamentNormalized } from '../lib/SportsBookModel/interfaces/ITournament';
import Category from './Category';

interface ISportPropTypes {
  sport: ISport;
  events: IEventNormalized[];
  categories: ICategoryNormalized[];
  tournaments: ITournamentNormalized[];
  onSelect: any;
  isActive: boolean;
}

interface ISportState {
  selectedCategory: number;
}

const ALL_CATEGORIES_SELECTED = 9999;

class Sport extends React.Component<ISportPropTypes> {
  categories: any;

  state: ISportState = {
    selectedCategory: -1,
  };

  isCategoryActive(i: number, selectedCategory: number): boolean {
    return selectedCategory === i || selectedCategory === ALL_CATEGORIES_SELECTED;
  }

  componentWillUpdate(nextProps: any, nextState: ISportState) {
    this.categories = this.props.categories.map((category, index) => {
      const tournaments = this.props.tournaments.filter(tournament => tournament.categoryId === category.id);
      const tournamentIds = tournaments.map(tournament => tournament.id);
      const events = this.props.events.filter(event => tournamentIds.includes(event.tournamentId));

      return <Category key={category.id} category={category} tournaments={tournaments} events={events}
                       onSelect={this.selectCategory(index)}
                       isActive={this.isCategoryActive(index, nextState.selectedCategory)}/>
    });
  }

  selectCategory(id: number): any {
    return () => {
      this.setState((state: ISportState) => ({
        selectedCategory: (state.selectedCategory === id) ? -1 : id
      }));
    };
  }

  toggleButtonStyle: CSSProperties = {
    float: 'right',
    margin: '10px',
  };

  toggleAllCategories(e: MouseEvent): void {
    const newSelection = (this.state.selectedCategory === ALL_CATEGORIES_SELECTED) ? -1 : ALL_CATEGORIES_SELECTED;
    this.setState({ selectedCategory: newSelection });
    e.preventDefault();
    e.stopPropagation();
  }

  render() {
    return (
      <Accordion fluid styled>
        <Accordion.Title active={this.props.isActive} onClick={this.props.onSelect}>
          <Icon name='dropdown'/>
          {this.props.sport.name} ({this.props.events.length})
          {this.props.isActive ?
            <button style={this.toggleButtonStyle} onClick={this.toggleAllCategories.bind(this)}>Toggle all
              categories</button> : ''}
        </Accordion.Title>
        <Accordion.Content active={this.props.isActive}>
          {this.categories}
        </Accordion.Content>
      </Accordion>
    );

  }
}

export default Sport;
