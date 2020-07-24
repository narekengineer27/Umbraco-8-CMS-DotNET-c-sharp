import produce, { Draft } from 'immer';
import * as _ from 'lodash';
import { BehaviorSubject } from 'rxjs';
import { IBaseItem } from '../SportsBookModel/interfaces/IBaseItem';
import DeepDiff from 'deep-diff';

export class Store<T extends IBaseItem> {
  private _items: T[] = [];
  private itemsSubject = new BehaviorSubject<T[]>([]);
  public items = this.itemsSubject.asObservable();

  public addOne(item: T): void {
    this.add([item]);
  }

  public addMany(items: T[]): void {
    this.add(items);
  }

  private isInStore(id: number | string): boolean {
    return !!_.find(this._items, [ 'id', id])
  }

  private findIndexById(id: number | string): number {
    return _.findIndex(this._items, ['id', id])
  }

  private add(items: T[]): void {
    this._items = produce(this._items, draft => {
      _.forEach(items, item => {
        const i = this.findIndexById(item.id);

        if (i !== -1) { // update
          draft.splice(i, 1, item as Draft<T>);
          // console.log('Store update', draft[i], item);
          // console.log('Diff: ', DeepDiff.diff(draft[i], item));
        } else { // add
          draft.push(item as Draft<T>);
        }
      })
    });
    
    this.itemsSubject.next(this._items);
  }
}

export default Store;
