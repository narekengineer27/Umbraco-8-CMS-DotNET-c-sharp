import React from 'react';
import './App.css';
import { GetHubConnectionDetails, GetUpcomingEvents, SubscribeToEventMonitoring } from './lib/SportsBookAPI';
import StoreFactory from './lib/SportBookState/StoreFactory';
import Store from './lib/SportBookState/Store';
import { IEvent, IEventNormalized } from './lib/SportsBookModel/interfaces/IEvent';
import { ITournamentNormalized } from './lib/SportsBookModel/interfaces/ITournament';
import { ICategoryNormalized } from './lib/SportsBookModel/interfaces/ICategory';
import { ISport } from './lib/SportsBookModel/interfaces/ISport';
import { INormalizedEntities, normalizeAll } from './lib/SportsBookModel/EntitiesNormalization';
import Sport from './components/Sport';
import { Container } from 'semantic-ui-react';

const storeFactory = StoreFactory.getInstance();

interface IAppState extends INormalizedEntities {
  selectedSport: number;
}

class App extends React.Component {
  sports: any;

  state: IAppState = {
    events: [],
    tournaments: [],
    categories: [],
    sports: [],
    selectedSport: -1,
  };
  eventStore: Store<IEventNormalized> = storeFactory('events');
  tournamentStore: Store<ITournamentNormalized> = storeFactory('tournaments');
  categoryStore: Store<ICategoryNormalized> = storeFactory('categories');
  sportStore: Store<ISport> = storeFactory('sports');

  constructor(props: any) {
    super(props);

    GetUpcomingEvents((events: IEvent[]): void => {
      this.updateStoresWithEvents(events);
      console.log('Initial events data received', events);
      },
      error => console.log('Server error', error)
    );

    GetHubConnectionDetails(
      (token, url) => {
        SubscribeToEventMonitoring(
          token,
          url,
          (event: IEvent): void => {
            console.log('Update for event', event.name, event);
            this.updateStoresWithEvents([event]);
          },
          error => console.log('Events monitoring update error', error)
        );
      },
      error => console.log('Hub connection details error', error)
    );
  }

  componentDidMount(): void {
    this.eventStore.items.subscribe(items => {
      this.setState({ events: items });
    });
    this.tournamentStore.items.subscribe(items => {
      this.setState({ tournaments: items })
    });
    this.categoryStore.items.subscribe(items => {
      this.setState({ categories: items })
    });
    this.sportStore.items.subscribe(items => {
      this.setState({ sports: items })
    });
  }

  componentWillUpdate(nextProps: any, nextState: IAppState) {
    // console.log('Component will update', nextProps, nextState);

    this.sports = nextState.sports.map((sport, index) => {
      // Following is not a good code. It will be refactored for sure
      const categories = nextState.categories.filter(category => category.sportId === sport.id);
      const categoryIds = categories.map(category => category.id);
      const tournaments = nextState.tournaments.filter(tournament => categoryIds.includes(tournament.categoryId));
      const tournamentIds = tournaments.map(tournament => tournament.id);
      const events = nextState.events.filter(event => tournamentIds.includes(event.tournamentId));

      return <Sport key={sport.id} categories={categories} tournaments={tournaments} events={events}
                    sport={sport} onSelect={this.selectSport(index)} isActive={nextState.selectedSport === index}/>
    });
  }

  updateStoresWithEvents(events: IEvent[]): void {
    const entitiesNormalized = normalizeAll(events);
    this.eventStore.addMany(entitiesNormalized.events);
    this.tournamentStore.addMany(entitiesNormalized.tournaments);
    this.categoryStore.addMany(entitiesNormalized.categories);
    this.sportStore.addMany(entitiesNormalized.sports);
  }

  selectSport(id: number): any {
    return () => {
      this.setState((state: IAppState) => ({
        selectedSport: (state.selectedSport === id) ? -1 : id
      }));
    };
  }

  render() {
    return <Container style={{ margin: 20 }}>
      {this.sports}
    </Container>;
  };
}

export default App;
