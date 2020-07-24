import { ISport } from './ISport';
import { IBaseItem } from './IBaseItem';

interface IBaseCategory extends IBaseItem {
  countryCode: any;
  tournaments: [];
}

export interface ICategory extends IBaseCategory {
  sport: ISport;
}

export interface ICategoryNormalized extends IBaseCategory {
  sportId: number;
}
