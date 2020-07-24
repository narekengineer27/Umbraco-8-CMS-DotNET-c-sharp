import { ITournament } from './ITournament';
import { IBaseItem } from './IBaseItem';
import { IMarketGroup } from './IMarketGroup';
import { IMarket } from './IMarket';

interface IBaseEvent extends IBaseItem {
  awayScore: number;
  competitors: any;
  eventTime: any;
  homeScore: number;
  isBookable: boolean;
  isBooked: boolean;
  lastUpdated: string; // '2019-06-18T21:42:41.999+00:00'
  marketGroups: IMarketGroup[];
  marketLines: any
  scheduledEndTime: string;
  scheduledStartTime: string; // '2019-06-18T22:30:00.000+00:00'
  status: number // some Enum?
  statusDescription: string;
  timestamp: number;
  topMarketLines: IMarket[];
  tournamentRound: any;
}

export interface IEvent extends IBaseEvent {
  tournament: ITournament;
}

export interface IEventNormalized extends IBaseEvent {
  tournamentId: number;
}
