import { IOutcome } from './IOutcome';

export interface IMarket {
  changeType: any;
  externalReference: string;
  hasBetStopFlag: boolean;
  id: number;
  isBettable: boolean;
  lastUpdated: string;
  name: string;
  outcomes: IOutcome[];
  overRound: number;
  parentId: number;
  parentType: number;
  sportEventStatusId: number;
  statusDescription: string;
  statusId: number;
  templateReference: number;
  timestamp: number;
}
