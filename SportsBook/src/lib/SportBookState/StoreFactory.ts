import Store from './Store';
import { IBaseItem } from '../SportsBookModel/interfaces/IBaseItem';

export class StoreFactory {
  private static instance: StoreFactory;

  private stores: { [id: string]: Store<any> } = {};

  private constructor() {
  }

  static getInstance(): <T extends IBaseItem>(key: string) => Store<T> {
    if (!StoreFactory.instance) {
      StoreFactory.instance = new StoreFactory();
    }

    return function returnStore<T extends IBaseItem>(key: string): Store<T> {
      if (!StoreFactory.instance.stores.hasOwnProperty(key)) {
        StoreFactory.instance.stores[key] = new Store<T>();
      }

      return StoreFactory.instance.stores[key];
    };
  }

}

export default StoreFactory;
