open System;

module Request =
    type Item =
    {   Id: int;
        Name: string;
        Comment: string;
        Options: Option list;
        Clauses: (condition: Condition * override: Option) list;    }
    and Option =
    {   Field: string; Value: string;   }
    and Condition =
    | When of (path: int list * value: string)
    | Not of Condition
    | And of Condition list
    | Or of Condition list


