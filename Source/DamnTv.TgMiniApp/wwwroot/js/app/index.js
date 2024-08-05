import { runElmishComponent } from "../sharing/libs/elmish/index.js";
const init = () => {
    return {
        model: { text: "TestElmish" },
        command: null
    };
};
const update = (model, message) => {
    switch (message.tag) {
        case "CHANGE_TEXT":
            return {
                model: { text: message.value },
                command: null
            };
    }
};
const view = (model, dispatch) => {
    let mainElement = document.createElement("div");
    let textSpan = document.createElement("span");
    textSpan.innerText = model.text;
    let inputText = document.createElement("input");
    inputText.addEventListener("change", () => dispatch({ tag: "CHANGE_TEXT", value: inputText.textContent }));
    mainElement.appendChild(textSpan);
    mainElement.appendChild(inputText);
    return mainElement;
};
const setView = (view) => {
    let body = document.querySelector("body");
    body.removeChild(body.children[0]);
    body.appendChild(view);
};
runElmishComponent({
    init: init,
    update: update,
    view: view
}, setView);
//# sourceMappingURL=index.js.map