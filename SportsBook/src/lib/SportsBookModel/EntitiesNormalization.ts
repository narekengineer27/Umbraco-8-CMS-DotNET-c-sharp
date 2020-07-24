import * as _ from 'lodash';
import produce from 'immer';
import { IBaseItem } from './interfaces/IBaseItem';
import { IEvent, IEventNormalized } from './interfaces/IEvent';
import { ITournament, ITournamentNormalized } from './interfaces/ITournament';
import { ICategory, ICategoryNormalized } from './interfaces/ICategory';
import { ISport } from './interfaces/ISport';

interface NormalizedItemAndExtract<N, E> {
  normalizedItem: N
  extract: E;
}

interface NormalizedItemsAndExtracts<N, E> {
  normalizedItems: N[];
  extracts: E[];
}

export interface INormalizedEntities {
  events: IEventNormalized[],
  tournaments: ITournamentNormalized[],
  categories: ICategoryNormalized[],
  sports: ISport[]
}

function normalizeItem<T, N, E>(item: T, idField: string, extractField: string): NormalizedItemAndExtract<N, E> {
  const normalizedItem: N = produce(item as unknown as N, draft => {
    // @ts-ignore
    delete draft[extractField];
    // @ts-ignore
    draft[idField] = item[extractField] ? item[extractField].id : 0;
  });

  return {
    normalizedItem: normalizedItem,
    // @ts-ignore
    extract: { ...item[extractField] as E }
  };
}

function normalizeItems<T, N, E extends IBaseItem>(items: T[], idField: string, extractField: string): NormalizedItemsAndExtracts<N, E> {
  const res: NormalizedItemsAndExtracts<N, E> = {
    normalizedItems: [],
    extracts: []
  };

  _.forEach(items, item => {
    const normalized = normalizeItem<T, N, E>(item, idField, extractField);

    res.normalizedItems.push(normalized.normalizedItem);
    res.extracts.push(normalized.extract);
  });

  return {
    normalizedItems: res.normalizedItems,
    extracts: _.uniqBy(res.extracts, extract => extract.id)
  };
}

export function normalizeEvents(events: IEvent[]): NormalizedItemsAndExtracts<IEventNormalized, ITournament> {
  return normalizeItems<IEvent, IEventNormalized, ITournament>(events, 'tournamentId', 'tournament');
}

export function normalizeTournaments(tournaments: ITournament[]): NormalizedItemsAndExtracts<ITournamentNormalized, ICategory> {
  return normalizeItems<ITournament, ITournamentNormalized, ICategory>(tournaments, 'categoryId', 'category');
}

export function normalizeCategories(categories: ICategory[]): NormalizedItemsAndExtracts<ICategoryNormalized, ISport> {
  return normalizeItems<ICategory, ICategoryNormalized, ISport>(categories, 'sportId', 'sport');
}

export function normalizeAll(events: IEvent[]): INormalizedEntities {
  const eventsNormalized = normalizeEvents(events);
  const tournamentsNormalized = normalizeTournaments(eventsNormalized.extracts);
  const categoriesNormalized = normalizeCategories(tournamentsNormalized.extracts);
  return {
    events: eventsNormalized.normalizedItems,
    tournaments: tournamentsNormalized.normalizedItems,
    categories: categoriesNormalized.normalizedItems,
    sports: categoriesNormalized.extracts,
  };
}
