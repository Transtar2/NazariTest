import type { NamedBean } from '../context/bean';
import { BeanStub } from '../context/beanStub';
import type { AgColumn } from '../entities/agColumn';
import type { AgProvidedColumnGroup } from '../entities/agProvidedColumnGroup';
import type { ColDef, ColGroupDef } from '../entities/colDef';
import type { ColumnEventType } from '../events';
import type { Column } from '../interfaces/iColumn';
export type ColKey<TData = any, TValue = any> = string | ColDef<TData, TValue> | Column<TValue>;
export type Maybe<T> = T | null | undefined;
export interface ColumnCollections {
    tree: (AgColumn | AgProvidedColumnGroup)[];
    treeDepth: number;
    list: AgColumn[];
    map: {
        [id: string]: AgColumn;
    };
}
export declare class ColumnModel extends BeanStub implements NamedBean {
    beanName: "colModel";
    private colDefs?;
    colDefCols?: ColumnCollections;
    cols?: ColumnCollections;
    private pivotMode;
    private showingPivotResult;
    private lastOrder;
    private lastPivotOrder;
    colSpanActive: boolean;
    ready: boolean;
    changeEventsDispatching: boolean;
    postConstruct(): void;
    private createColsFromColDefs;
    refreshCols(newColDefs: boolean): void;
    private selectCols;
    getColsToShow(): AgColumn[];
    refreshAll(source: ColumnEventType): void;
    setColsVisible(keys: (string | AgColumn)[], visible: boolean | undefined, source: ColumnEventType): void;
    private restoreColOrder;
    private positionLockedCols;
    private saveColOrder;
    getColumnDefs(): (ColDef | ColGroupDef)[] | undefined;
    private setColSpanActive;
    isPivotMode(): boolean;
    private setPivotMode;
    isPivotActive(): boolean;
    recreateColumnDefs(source: ColumnEventType): void;
    setColumnDefs(columnDefs: (ColDef | ColGroupDef)[], source: ColumnEventType): void;
    destroy(): void;
    getColTree(): (AgColumn | AgProvidedColumnGroup)[];
    getColDefColTree(): (AgColumn | AgProvidedColumnGroup)[];
    getColDefCols(): AgColumn[] | null;
    getCols(): AgColumn[];
    getAllCols(): AgColumn[];
    getColsForKeys(keys: ColKey[]): AgColumn[];
    getColDefCol(key: ColKey): AgColumn | null;
    getCol(key: Maybe<ColKey>): AgColumn | null;
    getColFromCollection(key: ColKey, cols?: ColumnCollections): AgColumn | null;
}