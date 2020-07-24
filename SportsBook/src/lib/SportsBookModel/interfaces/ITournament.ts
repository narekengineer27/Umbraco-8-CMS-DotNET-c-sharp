import { ICategory } from './ICategory';
import { IBaseItem } from './IBaseItem';

interface IBaseTournament extends IBaseItem {
  currentSeason: any;
}

export interface ITournament extends IBaseTournament {
  category: ICategory;
}

export interface ITournamentNormalized extends IBaseTournament {
  categoryId: number;
}
