import { IProduct } from './iproduct';

export interface IProducts {
    totalItemsCount: number;
    renderedItemsCount: number;
    items: IProduct[];
}
