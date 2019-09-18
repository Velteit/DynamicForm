module NTree =
    type Node<'T> =
    | Leaf of (data: 'T option)
    | Root of (data: 'T option * nodes: Node<'T> list)

    let inline empty = Option.None |> Node.Leaf

    let inline createLeaf = (Option.Some >> Node.Leaf)

    let inline createRoot = (Option.Some >> (fun v -> (v, [])) >> Node.Leaf)

    let inline addAfter value = function
        | Node.Leaf(d) -> Node.Root(d, [create value])
        | Node.Root(d, nodes) -> Node.Root(d, nodes::(create value))

    let inline addBefore value tree =
        Node.Root(Option.Some(value), [tree])

    let rec find func node =
        let rec findNode = function
            | head::tail ->
                let result = find func head;
                match result with | Option.Some(_) -> result | _ -> findNode tail
            | [] -> Option.None
        match node with
        | Node.Leaf(v) ->
            if func(v) then Option.Some(node) else Option.None
        | Node.Root(v, nodes) ->
            if func(v)
            then Option.Some(node)
            else nodes |> findNode
