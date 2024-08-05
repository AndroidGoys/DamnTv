

export type Command<TMessage> = Array<TMessage>

export type ChangeStateResult<TModel, TMessage> =
{
    model: TModel
    command: Command<TMessage> | null | undefined
}

export type InitFunction<TModel, TMessage> = () => ChangeStateResult<TModel, TMessage>;

export type UpdateFunction<TModel, TMessage> =
    (model: TModel, message: TMessage) => ChangeStateResult<TModel, TMessage>

export type ViewFunction<TModel, TMessage, TView> =
    (model: TModel, dispatch: (message: TMessage) => void) => TView

export type SetViewFunction<TView> = 
    (view: TView) => void

export interface IElmishComponent<TModel, TMessage, TView> {
    init: InitFunction<TModel, TMessage>,
    update: UpdateFunction<TModel, TMessage>,
    view: ViewFunction<TModel, TMessage, TView>
}

export function runElmishComponent<TModel, TMessage, TView>(
    component: IElmishComponent<TModel, TMessage, TView>,
    setView: SetViewFunction<TView>
) {
    let dispatchQueue = new Array<TMessage>();
    let { model, command } = component.init();

    if (command != null)
        dispatchQueue.push(...command)

    dispatch(null)
    
    function dispatch(message: TMessage | null) {
        if (message != null) 
            dispatchQueue.push(message)
        
        while (dispatchQueue.length > 0) {
            ({ model, command } = component.update(model, dispatchQueue[0]));
            dispatchQueue.splice(0, 1);

            if (command != null)
                dispatchQueue.push(...command)
            
        }

        let view = component.view(model, dispatch);
        setView(view)
    }
}