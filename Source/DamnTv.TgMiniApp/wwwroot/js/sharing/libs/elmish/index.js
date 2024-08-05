export function runElmishComponent(component, setView) {
    let dispatchQueue = new Array();
    let { model, command } = component.init();
    if (command != null)
        dispatchQueue.push(...command);
    dispatch(null);
    function dispatch(message) {
        if (message != null)
            dispatchQueue.push(message);
        while (dispatchQueue.length > 0) {
            ({ model, command } = component.update(model, dispatchQueue[0]));
            dispatchQueue.splice(0, 1);
            if (command != null)
                dispatchQueue.push(...command);
        }
        let view = component.view(model, dispatch);
        setView(view);
    }
}
//# sourceMappingURL=index.js.map