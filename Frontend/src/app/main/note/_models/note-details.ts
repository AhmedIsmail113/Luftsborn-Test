import { TagBasic } from "../../tag/_models/tag-basic";
import { NoteBasic } from "./note-basic";

export interface NoteDetails extends NoteBasic {
    tag?: TagBasic;
    createdOn?: string;
    deletedOn?: Date;
    modifiedOn?: Date;
}