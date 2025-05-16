import { NoteBasic } from "../../note/_models/note-basic";
import { TagBasic } from "./tag-basic";

export interface TagDetails extends TagBasic {
    Notes?: NoteBasic[];
}