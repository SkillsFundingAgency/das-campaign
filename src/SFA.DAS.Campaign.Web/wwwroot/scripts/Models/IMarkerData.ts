import { IMarker } from './IMarker';

/**
 * This is how a marker should
 * look like with a little bit more data in it
 * 
 * @interface IMarkerData
 */
export interface IMarkerData extends IMarker {
    Title: string;
    ShortDescription: string;
    Location: IMarker;
}