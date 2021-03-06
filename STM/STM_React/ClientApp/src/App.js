﻿import React, { Component, useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import swal from 'sweetalert';
import Moment from 'moment';
import go, { Binding } from 'gojs';
var $ = require('jquery');
window.jQuery = $;
require('jquery-autocomplete');

var sessionStorage_transfer = function (event) {
    if (!event) { event = window.event; }
    if (!event.newValue) return;
    if (event.key == 'getSessionStorage') {
        localStorage.setItem('sessionStorage', JSON.stringify(sessionStorage));
        localStorage.removeItem('sessionStorage');
    } else if (event.key == 'sessionStorage' && !sessionStorage.length) {
        var data = JSON.parse(event.newValue);
        for (var key in data) {
            sessionStorage.setItem(key, data[key]);
        }
    }
};

if (window.addEventListener) {
    window.addEventListener("storage", sessionStorage_transfer, false);
} else {
    window.attachEvent("onstorage", sessionStorage_transfer);
};

if (!sessionStorage.length) {
    localStorage.setItem('getSessionStorage', 'foobar');
    localStorage.removeItem('getSessionStorage', 'foobar');
};


export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/Projects' component={Projects} />
                <Route path='/Project' component={ProjectTasks} />
                <Route exact path='/Tasks' component={TaskList} />
                <Route path='/Task' component={Task} />
                <Route path='/Setup' component={Setup} />
                <Route path='/Boards' component={Board} />
                <Route path='/counter' component={Counter} />
                <Route path='/account/login' component={AccountLogin} />
                <Route path='/account/register' component={AccountRegister} />
                <Route path='/Report' component={Report} />
            </Layout>
        );
    }
}

class Button extends Component {
    constructor(props) {
        super(props);
        this.state = {};
        //this.handleClick = this.handleClick.bind(this);
    }

    //handleClick() {
    //    this.props.onClick();
    //}

    render() {
        return (
            <a className={"stm-btn " + this.props.classNames} onClick={this.props.onClick}>{this.props.caption}</a>
        );
    }
}

class EntitySelect extends Component {
    constructor(props) {
        super(props);
        this.state = {
            value: this.props.value,
            source: this.props.source ? this.props.source : [],
        };

        if (this.props.getSource) {
            this.props.getSource().then(json => {
                this.setState({ source: json })
            });
        }

        this.onChange = this.onChange.bind(this);
    }

    onChange = (e) => {
        let val = e.target.value;
        this.setState({
            value: val
        });
    }

    render() {
        let options = this.state.source.map(item => <option value={item.id}>{item.name}</option>);
        let s = { width: (this.props.width ? this.props.width : "100%")};
        return (
            <div className="d-flex align-items-center justify-content-end mb-2" onBlur={this.props.onBlur ? this.props.onBlur : null}>
                <span style={{flexGrow: "1"}} className={"mr-1 text-nowrap text-" + (this.props.captionDirection ? this.props.captionDirection : "right") + " " + (this.props.caption ? "" : "d-none")}>{this.props.caption}:</span>
                <div style={s} className="position-relative">
                    <select
                        readOnly={this.props.readonly ? true : false}
                        autoComplete="off"
                        className={"stm-text " + this.props.classNames}
                        onChange={this.props.onChange ? this.props.onChange : this.onChange}
                        name={this.props.name}
                        value={this.state.value != undefined ? this.state.value : this.props.value}
                        size={this.props.size ? this.props.size : "0"}>
                        {options}
                    </select>
                </div>
            </div>
        )
    }
}

class Text extends Component {
    constructor(props) {
        super(props);
        this.state = {
            value: this.props.onChange ? undefined : this.props.value,
            placeholder: this.props.placeholder,
            caption: this.props.caption,
            getSource: this.props.source,
            suggestDisplayed: false,
            source: null,
            selectedId: null,
        };

        this.onChange = this.onChange.bind(this);
        this.onDocumentMouseDown = this.onDocumentMouseDown.bind(this);
        this.showSuggest = this.showSuggest.bind(this);
        this.hideSuggest = this.hideSuggest.bind(this);
    }

    onChange = (e) => {
        let val = e.target.value;
        this.setState({
            value: val
        });

        if (this.state.getSource) {
            let s = null;
            this.state.getSource().then(json => {
                let s = json.filter(item => item.name.indexOf(val) != -1);
                this.setState({
                    source: s
                });
            });
        }
    }

    onDocumentMouseDown = event => {
        if (event.target.parentNode.classList.contains("stm-suggest")) {
            this.setState({
                suggestClick: true
            });

            return;
        }

        this.setState({
            suggestClick: false
        });
    };

    componentDidMount() {
        if (this.state.getSource) {
            document.addEventListener('mousedown', this.onDocumentMouseDown);
            document.addEventListener('mouseup', this.onDocumentMouseUp);
        }
    }

    componentWillUnmount() {
        document.removeEventListener('mousedown', this.onDocumentMouseDown);
        document.removeEventListener('mouseup', this.onDocumentMouseUp);
    }

    hideSuggest(e){
        if (!this.state.suggestClick)
            this.setState({ suggestDisplayed: false });

        if (this.props.onBlur) {
            this.props.onBlur(e);
        }
    }

    showSuggest(){
        this.setState({ suggestDisplayed: true });
        this.state.getSource().then(json => {
            this.setState({
                source: json,
            });
        });
    }

    render() {
        let s = { width: (this.props.width ? this.props.width : "100%")};

        let select = (item) => this.setState({
            value: item.name,
            suggestDisplayed: false,
            selectedId: item.id,
        });
        let suggest = this.state.getSource ?
            <div>
                <div className="" className={"stm-suggest " + (this.state.suggestDisplayed ? "" : "d-none")}>
                    {this.state.source ? this.state.source.map(item => <div
                        id={item.id}
                        className="stm-suggest-item"
                        onClick={function () {
                            select(item);
                        }} >
                        {item.name}
                    </div>) : null}
                </div>
                <input type="hidden" name={this.props.suggestName} value={this.state.selectedId} />
            </div>
            : <div></div>;

        return (
            <div className="d-flex align-items-center justify-content-end mb-2" onBlur={this.props.onBlur ? this.props.onBlur : null}>
                <span
                    style={{ flexGrow: "1" }}
                    className={"mr-1 text-"
                        + (this.props.captionDirection ? this.props.captionDirection : "right")
                        + (this.props.nowrap ? " text-nowrap " : "")
                        + (this.state.caption ? "" : " d-none")}>
                    {this.state.caption}:
                    </span>
                <div style={s} className="position-relative">
                    <input
                        readOnly={this.props.readonly ? true : false}
                        autocomplete="off"
                        type={this.props.type ? this.props.type : "text"}
                        onChange={this.props.onChange ? this.props.onChange : this.onChange}
                        className={"stm-text " + this.props.classNames}
                        name={this.props.name}
                        placeholder={this.state.placeholder}
                        value={this.state.value != undefined ? this.state.value : this.props.value}
                        onBlur={this.props.source ? this.hideSuggest : this.props.onBlur ? this.props.onBlur : null}
                        onFocus={this.props.source ? this.showSuggest : null}
                    />
                    {suggest}
                </div>
            </div>
        )
    }
}

class TextArea extends Component {
    constructor(props) {
        super(props);
        this.state = {
            value: this.props.onChange ? undefined : this.props.value,
            placeholder: this.props.placeholder,
            caption: this.props.caption
        };

        this.onChange = this.onChange.bind(this);
    }

    onChange = (e) => {
        this.setState({
            value: e.target.value,
        })
    }

    render() {
        let s = { width: (this.props.width ? this.props.width : "100%")};
        return (
            <div className="d-flex align-items-center justify-content-end mb-2">
                <span style={{ flexGrow: "1" }}  className={"mr-1 text-nowrap text-" + (this.props.captionDirection ? this.props.captionDirection : "right") + " " + (this.state.caption ? "" : "d-none")}>{this.state.caption}:</span>
                <textarea style={s} rows={this.props.rows ? this.props.rows : 5} cols={this.props.cols ? this.props.cols : 5} onChange={this.props.onChange ? this.props.onChange : this.onChange} className={"stm-text " + this.props.classNames} name={this.props.name} placeholder={this.state.placeholder} value={this.state.value != undefined ? this.state.value : this.props.value} />
            </div>
        )
    }
}

class Popup extends Component {
    constructor(props) {
        super(props);
        this.onSubmit = this.onSubmit.bind(this);
        this.handleUserInput = this.handleUserInput.bind(this);
    }

    handleUserInput = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value });
    }

    onSubmit = (e) => {
        if (this.props.action) {
            let object = {};
            let formData = new FormData(e.target);
            formData.forEach(function (value, key) {
                object[key] = value;
            });


            if (this.props.addToSubmit) {
                object = this.props.addToSubmit(object);
            }

            var json = JSON.stringify(object);
            fetch(this.props.action, {
                method: this.props.method,
                body: json,
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                },
            }).then(response => {
                if (!response.ok) {
                    response.text().then(message => swal({
                        title: "Ошибка",
                        text: message,
                        icon: "error",
                    }));
                    throw new Error("Breaked");
                }
            }).cath(message => console.log(message));
        }

        this.props.onOk();
    }

    render() {
        let dialog = (
            <div>
                <div className="stm-mask"></div>
                <div className="stm-popup-container">
                    <div className="stm-popup">
                        <div className="stm-popup-header">
                            {this.props.title}
                            <span className="stm-popup-close" onClick={this.props.onCancel}></span>
                        </div>
                        <form onSubmit={this.onSubmit} >
                            <div className="stm-popup-body">
                                {this.props.children}
                            </div>
                            <div className="stm-popup-footer d-flex justify-content-end">
                                <input type="submit" className="stm-btn ml-3 stm-btn-thin" value={this.props.okText ? this.props.okText : "Ок"} />
                                <button className="stm-btn ml-3 stm-btn-thin stm-btn-red" onClick={this.props.onCancel}>
                                    {this.props.cancelText ? this.props.cancelText : "Отмена"}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        );

        if (!this.props.isOpen)
            dialog = null;

        return (
            <div>
                {dialog}
            </div>
        )
    }
}

class Relations extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        let onDeleteRel = this.props.onDeleteRel;
        let rels = this.props.rels ? this.props.rels.map(item =>
            <div className="row p-2">
                <div className="col-4">{item.taskSlave.code + " " + item.taskSlave.name}</div>
                <div className="col-4">{item.taskMaster.code + " " + item.taskMaster.name}</div>
                <div className="col-2">{item.relType}</div>
                <div className="col-2"><a class="stm-btn stm-btn-link" onClick={function () {
                    swal({
                        title: "Подтверждение удаления",
                        text: "Вы действительно хотите удалить выбранную запись?",
                        icon: "warning",
                        buttons: {
                            confirm: {
                                text: "Удалить",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            },
                            cancel: {
                                text: "Отмена",
                                value: null,
                                visible: true,
                                className: "",
                                closeModal: true,
                            },
                        }
                    }).then(function (result) {
                        if (result) {
                            fetch('api/taskrels/' + item.id, {
                                method: 'DELETE',
                                headers: {
                                    'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
                                }
                            });

                            onDeleteRel(item.id);
                        }
                    });
                }}>Удалить</a></div>
            </div>
        ) : null;
        return (
            <div>
                <h3>Связи</h3>
                <div className="stm-table mb-3">
                    <div className="stm-table-header row">
                        <div className="col-4 text-left">Зависимая задача</div>
                        <div className="col-4 text-left">Задача</div>
                        <div className="col-2 text-left">Тип связи</div>
                        <div className="col-2 text-left"></div>
                    </div>
                    {rels}
                </div>
                <TaskRelsPopupContainer />
            </div>
        )
    }
}

class TaskRelsPopupContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            popupOpen: false,
        }
    }

    render() {
        let that = this;
        return (
            <div className="mb-2">
                <Button classNames="stm-btn-thin" caption="Добавить" onClick={() => this.setState({ popupOpen: true })} />
                <Popup
                    title="Создать статус"
                    addToSubmit={(obj) => {
                        return obj;
                    }} action="api/taskrels" method="POST" isOpen={this.state.popupOpen} onOk={(e) => {
                        this.setState({ popupOpen: false });
                    }}
                    onCancel={(e) => {
                        this.setState({ popupOpen: false })
                    }}>
                    <EntitySelect
                        name="relType"
                        caption="Тип связи"
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            let promise = new Promise(function (resolve) {
                                resolve([
                                    { name: "Блокируется", id: "Блокируется" },
                                    { name: "Родительская задача", id: "Родительская задача" }
                                ]);
                            });

                            return promise;
                        }}
                    />
                    <EntitySelect
                        name="taskSlaveId"
                        caption='Зависимая задача'
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            return fetch('/api/tasks/lite', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json()).then(json => json.map(i => {
                                return {
                                    name: i.code + " " + i.name,
                                    id: i.id
                                }
                            }));
                        }}
                    />
                    <EntitySelect
                        name="taskMasterId"
                        caption='Задача'
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            return fetch('/api/tasks/lite', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json()).then(json => json.map(i => {
                                return {
                                    name: i.code + " " + i.name,
                                    id: i.id
                                }
                            }));
                        }}
                    />
                </Popup>
            </div>
        );
    }
}

class Report extends Component {
    constructor(props) {
        super(props);
        this.state = {};

        this.color = 0;
        this.drawWF = this.drawWF.bind(this);
        this.getNextColor = this.getNextColor.bind(this);
        this.deleteTask = this.deleteTask.bind(this);
        this.deleteRel = this.deleteRel.bind(this);

        fetch('/api/tasks/lite', {
            method: "GET",
            headers: {
                Authorization: "Bearer " + sessionStorage.getItem("token")
            }
        })
            .then(response => response.json())
            .then(json => {
                let _tasks = json.map(item => {
                    return {
                        name: item.name,
                        code: item.code,
                        id: item.id,
                        date: item.plannedStart,
                        weight: item.storyPoints
                    }
                });

                this.setState({
                    tasks: _tasks
                });

                fetch('/api/taskrels/BackwardDirected', {
                    method: "GET",
                    headers: {
                        Authorization: "Bearer " + sessionStorage.getItem("token")
                    }
                })
                    .then(response => response.json())
                    .then(json => {
                        this.setState({
                            rels: json
                        });

                        fetch('api/taskrels/maxpath', {
                            method: "GET"
                        })
                            .then(r => r.json())
                            .then(r => {
                                let _rels = this.state.rels;
                                for (let i = 0; i < r.length - 1; i++) {
                                    let rel = _rels.find(item => item.taskMasterId == r[i] && item.taskSlaveId == r[i + 1]);
                                    rel.color = "#C0392B";
                                    rel.stroke = "5";
                                }

                                this.setState({
                                    rels: _rels
                                })

                                this.drawWF();
                            });
                    });
            });
    }

    deleteRel(id) {
        let _rels = this.state.rels.filter(r => r.id != id);
        this.setState({
            rels: _rels
        });

        this.drawWF();
    }

    deleteTask(id) {
        let _tasks = this.state.tasks.filter(t => t.id != id);
        let _rels = this.state.rels.filter(r => r.taskSlaveId != id && r.taskMasterId != id);
        this.setState({
            tasks: _tasks,
            rels: _rels
        });

        this.drawWF();
    }

    drawWF() {
        this.state.d ? this.state.d.div = null : null;

        let $ = go.GraphObject.make;
        let diagram = $(go.Diagram, "graph");
        let nodes = this.state.tasks.map(item => {
            return {
                key: item.id,
                text: item.code + " " + item.name + " " + Moment(item.date).format("DD.MM.YYYY"),
                color: this.getNextColor()
            }
        });

        let links = this.state.rels.map(item => {
            return {
                from: item.taskMaster.id,
                to: item.taskSlave.id,
                text: item.taskMaster.storyPoints,
                color: item.color ? item.color : "black",
                stroke: item.stroke
            }
        });

        diagram.linkTemplate =
            $(go.Link,
                $(go.Shape, new go.Binding("stroke", "color"), new go.Binding("strokeWidth", "stroke")),                           // this is the link shape (the line)
                $(go.Shape, { toArrow: "Standard" }, new go.Binding("stroke", "color")),  // this is an arrowhead
                $(go.TextBlock,                        // this is a Link label
                    new go.Binding("text", "text"),
                    { segmentOffset: new go.Point(0, -6) }
                )
            );

        diagram.model = new go.GraphLinksModel(nodes, links);
        diagram.nodeTemplate = $(go.Node, "Auto",
            $(go.Shape, "RoundedRectangle", { fill: "White" }, new go.Binding("fill", "color")),
            $(go.TextBlock, { margin: 5, width: 120 }, "text", new go.Binding("text", "text")),
        );
        diagram.layout = $(go.TreeLayout);

        this.setState({
            d: diagram
        })
    }

    getNextColor() {
        let colors = ["#1abc9c", "#3498DB", "#9B59B6", "#F1C40F", "#E74C3C", "#2ECC71"];
        let color = colors[this.color];
        this.color = (this.color + 1) > 5 ? 0 : this.color + 1;

        return color;
    }

    render() {

        return (
            <div>
                <h3>Расчет критического пути</h3>
                <a class="stm-btn stm-btn-orange" onClick={function () {
                    fetch('api/taskrels/SetTimes', {
                        method: "GET"
                    });
                }} >Автоматический расчет дат</a>
                <div id="graph" style={{ height: "600px" }}></div>
                <Relations rels={this.state.rels} onDeleteRel={this.deleteRel} />
                <TaskList onDeleteTask={this.deleteTask} />
            </div>
        )
    }
}

class Setup extends Component {
    constructor(props) {
        super(props);
        this.state = {};
        this.color = 0;

        this.drawWF = this.drawWF.bind(this);
        this.getNextColor = this.getNextColor.bind(this);

        fetch("/api/taskstatus", {
            method: 'GET',
            headers: {
                Authorization: "Bearer " + sessionStorage.getItem("token")
            }
        }).then(response => response.json())
            .then(json => {
                this.setState({
                    statuses: json
                });

                fetch('/api/workflows', {
                    method: 'GET',
                    headers: {
                        Authorization: "Bearer " + sessionStorage.getItem("token")
                    }
                }).then(response => response.json())
                    .then(json => {
                        this.setState({
                            workflows: json
                        })
                    }).then(() => {
                        this.drawWF();
                    });
            })
    }

    drawWF() {
        this.state.d ? this.state.d.div = null : null;

        let $ = go.GraphObject.make;
        let diagram = $(go.Diagram, "graph");
        let nodes = this.state.statuses.map(item => {
            return {
                key: item.name,
                color: this.getNextColor()
            }
        });

        let links = this.state.workflows.map(item => {
            return {
                from: item.statusFrom.name,
                to: item.statusTo.name
            }
        });

        diagram.model = new go.GraphLinksModel(nodes, links);
        diagram.nodeTemplate = $(go.Node, "Auto",
            $(go.Shape, "RoundedRectangle", { fill: "White" }, new go.Binding("fill", "color")),
            $(go.TextBlock, {margin: 5},"text", new go.Binding("text", "key")),
        );
        diagram.layout = $(go.CircularLayout);

        this.setState({
            d: diagram
        })
    }

    getNextColor() {
        let colors = ["#1abc9c", "#3498DB", "#9B59B6", "#F1C40F", "#E74C3C", "#2ECC71"];
        let color = colors[this.color];
        this.color = (this.color + 1) > 5 ? 0 : this.color + 1;

        return color;
    }

    render() {
        let that = this;
        let statuses = this.state.statuses ? this.state.statuses.map(item => {
            return (
                <div className="stm-board-row row p-2">
                    <div className="col"><img src={item.icon}/></div>
                    <div className="col">{item.name}</div>
                    <div className="col">{item.stage}</div>
                    <div className="col text-right"><a class="stm-btn stm-btn-link" onClick={function () {
                        swal({
                            title: "Подтверждение удаления",
                            text: "Вы действительно хотите удалить выбранную запись?",
                            icon: "warning",
                            buttons: {
                                confirm: {
                                    text: "Удалить",
                                    value: true,
                                    visible: true,
                                    className: "",
                                    closeModal: true
                                },
                                cancel: {
                                    text: "Отмена",
                                    value: null,
                                    visible: true,
                                    className: "",
                                    closeModal: true,
                                },
                            }
                        }).then(function (result) {
                            if (result) {
                                fetch('api/taskstatus/' + item.id, {
                                    method: 'DELETE',
                                    headers: {
                                        'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
                                    }
                                });

                                let newStat = that.state.statuses.filter(s => s.id != item.id);
                                let newWFs = that.state.workflows.filter(wf => wf.statusFromId != item.id && wf.statusToId != item.id)
                                that.setState({
                                    statuses: newStat,
                                    workflows: newWFs
                                });

                                that.drawWF();
                            }
                        });
                    }}>Удалить</a></div>
                </div>
            )
        }) : null;

        let workflows = this.state.workflows ? this.state.workflows.map(item => {
            return (
                <div className="stm-board-row row p-2">
                    <div className="col">{item.role.name}</div>
                    <div className="col">{item.statusFrom.name}</div>
                    <div className="col">{item.statusTo.name}</div>
                    <div className="col text-right"><a class="stm-btn stm-btn-link" onClick={function () {
                        swal({
                            title: "Подтверждение удаления",
                            text: "Вы действительно хотите удалить выбранную запись?",
                            icon: "warning",
                            buttons: {
                                confirm: {
                                    text: "Удалить",
                                    value: true,
                                    visible: true,
                                    className: "",
                                    closeModal: true
                                },
                                cancel: {
                                    text: "Отмена",
                                    value: null,
                                    visible: true,
                                    className: "",
                                    closeModal: true,
                                },
                            }
                        }).then(function (result) {
                            if (result) {
                                fetch('api/workflows/' + item.id, {
                                    method: 'DELETE',
                                    headers: {
                                        'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
                                    }
                                });
                                let newWFs = that.state.workflows.filter(wf => wf.id != item.id)
                                that.setState({
                                    workflows: newWFs
                                });

                                that.drawWF();
                            }
                        });
                    }}>Удалить</a></div>
                </div>
            )
        }) : null;

        return (
            <div>
                <h3>Workflow</h3>
                <div id="graph"></div>
                <div className="row">
                    <div className="col-lg-6 col-md-12">
                        <h3>Статусная модель</h3>
                        <div className="stm-table mb-3 col-12">
                            <div className="stm-table-header row">
                                <div className="col text-left">Роль</div>
                                <div className="col text-left">Статус "Из"</div>
                                <div className="col text-left">Статус "В"</div>
                                <div className="col text-left"></div>
                            </div>
                            {workflows}
                        </div>
                        <WorkflowPopupContainer />
                    </div>
                    <div className="col-lg-6 col-md-12">
                        <h3>Статусы</h3>
                        <div className="stm-table mb-3 col-12">
                            <div className="stm-table-header row">
                                <div className="col text-left">Иконка</div>
                                <div className="col text-left">Название</div>
                                <div className="col text-left">Этап</div>
                                <div className="col text-left"></div>
                            </div>
                            {statuses}
                        </div>
                        <StatusPopupContainer />
                    </div>
                </div>
            </div>
        )
    }
}

class WorkflowPopupContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            popupOpen: false,
        }
    }

    render() {
        let that = this;
        return (
            <div className="mb-2">
                <Button classNames="stm-btn-thin" caption="Добавить" onClick={() => this.setState({ popupOpen: true })} />
                <Popup
                    title="Создать статус"
                    addToSubmit={(obj) => {
                        return obj;
                    }} action="api/workflows" method="POST" isOpen={this.state.popupOpen} onOk={(e) => {
                        this.setState({ popupOpen: false });
                    }}
                    onCancel={(e) => {
                        this.setState({ popupOpen: false })
                    }}>
                    <EntitySelect
                        name="roleId"
                        caption="Роль"
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            return fetch('/api/roles', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json());
                        }}
                    />
                    <EntitySelect
                        name="statusFromId"
                        caption='Статус "Из"'
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            return fetch('/api/taskstatus', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json());
                        }}
                    />
                    <EntitySelect
                        name="statusToId"
                        caption='Статус "В"'
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            return fetch('/api/taskstatus', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json());
                        }}
                    />
                </Popup>
            </div>
        );
    }
}

class StatusPopupContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            popupOpen: false,
        }
    }

    render() {
        let that = this;
        return (
            <div className="mb-2">
                <Button classNames="stm-btn-thin" caption="Добавить" onClick={() => this.setState({ popupOpen: true })} />
                <Popup
                    title="Создать статус"
                    addToSubmit={(obj) => {
                        return obj;
                    }} action="api/taskstatus" method="POST" isOpen={this.state.popupOpen} onOk={(e) => {
                        this.setState({ popupOpen: false });
                    }}
                    onCancel={(e) => {
                        this.setState({ popupOpen: false })
                    }}>
                    <Text
                        name="name"
                        caption="Название"
                        captionDirection="left"
                        width="75%"
                    />
                    <EntitySelect
                        name="stage"
                        caption="Стадия"
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            let promise = new Promise(function (resolve) {
                                resolve([
                                    { name: "Открыто", id: "open" },
                                    { name: "В работе", id: "in progress" },
                                    { name: "Завершено", id: "done" }
                                ]);
                            });

                            return promise;
                        }}
                    />
                </Popup>
            </div>
        );
    }
}


class Board extends Component {
    constructor(props) {
        super(props);
        this.state = {};
        this.json2task = this.json2task.bind(this);
        fetch('/api/tasks/board', {
            method: "GET",
            headers: {
                Authorization: 'Bearer ' + sessionStorage.getItem('token'),
            }
        })
            .then(response => {
                if (response.redirected) {
                    window.location.href = response.url;
                    throw new Error('Breaked');
                }

                return response;
            })
            .then(response => {
                return response.json()
            })
            .then(json => {
                let _active = json.filter(item => item.status.stage == 'in progress');
                let _open = json.filter(item => item.status.stage == 'open');
                let _done = json.filter(item => item.status.stage == 'done');
                this.setState({
                    active: _active,
                    open: _open,
                    done: _done,
                });
            })
            .catch((e) => console.log(e));
    }

    json2task(json) {
        return (
            <a href={"/Task/" + json.id} className="stm-board-task mb-3">
                <div className="d-flex align-items-center">
                    <div className=""><img src={json.priority.icon} /></div>
                    <div className="">{json.code}</div>
                    <div className="ml-auto">{json.assignee.lastName + " " + json.assignee.firstName.substr(0, 1) + "."}</div>
                </div>
                <div className="d-flex align-items-center">
                    <div className=""><img src={json.type.icon} /></div>
                    <div className="">{json.name}</div>
                    <div className={"ml-auto stm-board-sp" + (!json.storyPoints ? " d-none":"")} >{json.storyPoints}</div>
                </div>
            </a>
        )
    }

    render() {
        let open = this.state.open ? this.state.open.map(item => this.json2task(item)) : null;
        let openPS = this.state.open ? this.state.open.reduce(function (result, item) {
            return result += item.storyPoints;
        }, 0) : null;

        let done = this.state.done ? this.state.done.map(item => this.json2task(item)) : null;
        let donePS = this.state.done ? this.state.done.reduce(function (result, item) {
            return result += item.storyPoints;
        }, 0) : null;

        let active = this.state.active ? this.state.active.map(item => this.json2task(item)) : null;
        let activePS = this.state.active ? this.state.active.reduce(function (result, item) {
            return result += item.storyPoints;
        }, 0) : null;

        return (
            <div className="stm-board row">
                <div className="col-4 stm-list-open">
                    <div className="d-flex align-items-end">
                        <h3>Открытые</h3>
                        <div className="h4 ml-auto stm-board-header-sp">{openPS}</div>
                    </div>
                    {open}
                </div>
                <div className="col-4 stm-list-active">
                    <div className="d-flex align-items-end">
                        <h3>В работе</h3>
                        <div className="h4 ml-auto stm-board-header-sp">{activePS}</div>
                    </div>
                    {active}
                </div>
                <div className="col-4 stm-list-done">
                    <div className="d-flex align-items-end">
                        <h3>Завершены</h3>
                        <div className="h4 ml-auto stm-board-header-sp">{donePS}</div>
                    </div>
                    {done}
                </div>
            </div>
        )
    }
}

class ProjectTasks extends Component {
    constructor(props) {
        super(props);
        let splitted = this.props.location.pathname.split('/');
        let _id = splitted[splitted.length - 1];
        this.state = {
            id: _id
        };
    }

    render() {
        return (
            <div>
                <TaskList projectId={this.state.id} />
            </div>
        );
    }
}

class Projects extends Component {
    constructor(props) {
        super(props);
        this.state = {
            projects: [],
            popupOpen: false,
            activeId: ""
        };

        this.reload = this.reload.bind(this);
        this.setActive = this.setActive.bind(this);
        this.reload();
    }

    setActive = (id) => {
        this.setState({
            activeId: id,
        })
    }

    reload = () => {
        let setActive = this.setActive;
        let activeId = this.state.activeId;
        fetch('api/Projects', {
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
            }
        })
            .then(response => {
                if (response.redirected) {
                    window.location.href = response.url;
                    throw new Error('Breaked');
                }

                return response;
            })
            .then(response => {
                return response.json()
            })
            .then(json => {
                this.setState({
                    projects: json
                })
            })
            .catch((e) => console.log(e));
    }

    render() {
        let activeId = this.state.activeId;
        let setActive = this.setActive;
        let projects = this.state.projects.map((item) => <Project item={item} key={item.id} onClick={setActive} isActive={item.id == activeId} />);
        let reload = this.reload;
        let that = this;
        return (
            <div>
                <h3>
                    Проекты
                </h3>
                <div className="stm-table mb-3">
                    <div className="stm-table-header row">
                        <div className="col text-center">Название</div>
                        <div className="col text-center">Префикс</div>
                        <div className="col text-center">Описание</div>
                        <div className="col text-center">Ответственный</div>
                    </div>
                    {projects}
                </div>
                <Button caption="Добавить" onClick={() => this.setState({ popupOpen: true })} />
                <Button caption="Удалить" classNames="ml-3 stm-btn-red" onClick={function () {
                    if (!activeId) {
                        swal({
                            title: 'Необходимо выбрать запись',
                            icon: "warning"
                        });
                        return;
                    }

                    swal({
                        title: "Подтверждение удаления",
                        text: "Вы действительно хотите удалить выбранную запись?",
                        icon: "warning",
                        buttons: {
                            confirm: {
                                text: "Удалить",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            },
                            cancel: {
                                text: "Отмена",
                                value: null,
                                visible: true,
                                className: "",
                                closeModal: true,
                            },
                        }
                    }).then(function (result) {
                        if (result) {
                            fetch('api/projects/' + activeId, {
                                method: 'DELETE',
                                headers: {
                                    'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
                                }
                            }).then(() => {
                                that.setState({ activeId: null });
                                reload();
                            });
                        }
                    });
                }}/>
                <Popup title="Создать проект" addToSubmit={(obj) => {
                    obj.manager = sessionStorage.getItem("id");

                    return obj;
                }} action="api/projects" method="POST" isOpen={this.state.popupOpen} onOk={(e) => {
                    this.setState({ popupOpen: false });
                    this.reload();
                }}
                    onCancel={(e) => {
                        this.setState({ popupOpen: false })
                    }}>
                    <Text placeholder="Название" width="70%" name="name" value="" caption="Название" classNames="" />
                    <Text placeholder="Префикс" width="70%" name="prefix" value="" caption="Префикс" classNames="" />
                    <TextArea placeholder="Описание" width="70%" name="description" value="" rows="10" cols="10" caption="Описание" classNames="" />
                </Popup>
            </div>
        );
    }
}

class Project extends Component {
    constructor(props) {
        super(props);
        let m = this.props.item.managerNavigation;
        this.state = {
            id: this.props.item.id,
            name: this.props.item.name,
            manager: (m.lastName ? (m.lastName + ' ') : '') + (m.firstName ? (m.firstName + ' ') : '') + (m.midName ? m.midName : ''),
            prefix: this.props.item.prefix,
            description: this.props.item.description
        };
    }

    render() {
        return (
            <div className={"row pb-2 pt-2 " + (this.props.isActive ? "stm-row-active" : "")} onClick={(e) => {
                this.props.onClick(this.state.id);
            }
            }>
                <div className="d-none">{this.state.id}</div>
                <div className="col text-center">{this.state.name}</div>
                <div className="col text-center">{this.state.prefix}</div>
                <div className="col text-center">{this.state.description}</div>
                <div className="col text-center">{this.state.manager}</div>
            </div>);
    }
}

//Задачи
class TaskList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            tasks: [],
            popupOpen: false,
            activeId: ""
        };

        this.reload = this.reload.bind(this);
        this.setActive = this.setActive.bind(this);
        this.reload();
    }

    setActive = (id) => {
        this.setState({
            activeId: id,
        })
    }

    reload = () => {
        let setActive = this.setActive;
        let activeId = this.state.activeId;
        fetch(this.props.projectId ? ("api/Tasks/Project/" + this.props.projectId) : ('api/Tasks'), {
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
            }
        })
            .then(response => {
                if (response.redirected) {
                    window.location.href = response.url;
                    throw new Error('Breaked');
                }

                return response;
            })
            .then(response => {
                return response.json();
            })
            .then(json => {
                console.log(json);
                this.setState({
                    tasks: json
                })
            })
            .catch((e) => console.log(e));
    }

    render() {
        let activeId = this.state.activeId;
        let setActive = this.setActive;
        let tasks = this.state.tasks.map((item) => <TaskListItem item={item} key={item.id} onClick={setActive} isActive={item.id == activeId} />);
        let reload = this.reload;
        let that = this;
        return (
            <div>
                <h3>
                    Задачи
                </h3>
                <div className="stm-table mb-3">
                    <div className="stm-table-header row">
                        <div className="col-1 text-left">Тип</div>
                        <div className="col-1 text-left">Приоритет</div>
                        <div className="col-1 text-left">Номер</div>
                        <div className="col-5 text-left">Название</div>
                        <div className="col-1 text-left">Статус</div>
                        <div className="col-2 text-left">Ответственный</div>
                        <div className="col-1 text-left">Story points</div>
                    </div>
                    {tasks}
                </div>
                <Button caption="Добавить" onClick={() => this.setState({ popupOpen: true })} />
                <Button caption="Удалить" classNames="ml-3 stm-btn-red" onClick={function () {
                    if (!activeId) {
                        swal({
                            title: 'Необходимо выбрать запись',
                            icon: "warning"
                        });
                        return;
                    }

                    swal({
                        title: "Подтверждение удаления",
                        text: "Вы действительно хотите удалить выбранную запись?",
                        icon: "warning",
                        buttons: {
                            confirm: {
                                text: "Удалить",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            },
                            cancel: {
                                text: "Отмена",
                                value: null,
                                visible: true,
                                className: "",
                                closeModal: true,
                            },
                        }
                    }).then(function (result) {
                        if (result) {
                            if (that.props.onDeleteTask) {
                                that.props.onDeleteTask(activeId);
                            }

                            fetch('/api/tasks/' + activeId, {
                                method: 'DELETE',
                                headers: {
                                    'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
                                }
                            }).then(() => {
                                that.setState({ activeId: null });
                                reload();
                            });
                        }
                    });
                }} />
                <Popup
                    title="Создать задачу"
                    addToSubmit={(obj) => {

                        obj.assigneeId = sessionStorage.getItem("id");
                        obj.createdById = sessionStorage.getItem("id");

                        return obj;
                }} action="api/tasks" method="POST" isOpen={this.state.popupOpen} onOk={(e) => {
                    this.setState({ popupOpen: false });
                    this.reload();
                }}
                    onCancel={(e) => {
                        this.setState({ popupOpen: false })
                    }}>

                    <EntitySelect
                        name="projectId"
                        caption="Проект"
                        value={this.props.projectId ? this.props.projectId : null}
                        readonly={this.props.projectId ? true : false}
                        width="70%"
                        getSource={function () {
                            return fetch('/api/projects', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json());
                        }}
                    />
                    <Text placeholder="Название" width="70%" name="name" value="" caption="Название" classNames="" />
                    <TextArea placeholder="Описание" width="70%" name="description" value="" rows="10" cols="10" caption="Описание" classNames="" />
                    <EntitySelect
                        name="typeId"
                        caption="Тип"
                        width="70%"
                        getSource={function () {
                            return fetch('/api/tasktypes', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json());
                        }}
                    />
                    <EntitySelect
                        name="statusId"
                        caption="Статус"
                        width="70%"
                        getSource={function () {
                            return fetch('/api/taskstatus', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json());
                        }}
                    />
                    <EntitySelect
                        name="priorityId"
                        caption="Статус"
                        width="70%"
                        getSource={function () {
                            return fetch('/api/taskpriorities', {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }).then(response => response.json());
                        }}
                    />
                    <Text
                        placeholder="Планируемая дата начала"
                        width="70%"
                        type="date"
                        name="plannedStart"
                        value=""
                        caption="Начало"
                        classNames="" />
                    <Text
                        placeholder="Планируемая дата окончания"
                        width="70%"
                        type="date"
                        name="plannedComplete"
                        value=""
                        caption="Окончание"
                        classNames="" />
                </Popup>
            </div>
        );
    }
}

class TaskListItem extends Component {
    constructor(props) {
        super(props);
        //let m = this.props.item.assignee;
        //this.state = {
        //    id: this.props.item.id,
        //    code: this.props.item.code,
        //    name: this.props.item.name,
        //    type: this.props.item.type ? this.props.item.type.name : '',
        //    priority: this.props.item.priority ? this.props.item.priority.name : '',
        //    status: this.props.item.status ? this.props.item.status.name : '',
        //    manager: m ? (m.lastName ? (m.lastName + ' ') : '') + (m.firstName ? (m.firstName + ' ') : '') + (m.midName ? m.midName : '') : '',
        //    prefix: this.props.item.prefix,
        //    description: this.props.item.description,
        //    storyPoints: this.props.item.storyPoints,
        //};
    }

    render() {
        return (
            <div className={"row pb-2 pt-2 " + (this.props.isActive ? "stm-row-active" : "")} onClick={(e) => {
                this.props.onClick(this.props.item.id);
            }
            }>
                <div className="d-none">{this.props.item.id}</div>
                <div className="col-1 text-left">{this.props.item.type}</div>
                <div className="col-1 text-left">{this.props.item.priority}</div>
                <div className="col-1 text-left"><a href={"/Task/" + this.props.item.id}>{this.props.item.code}</a></div>
                <div className="col-5 text-left">{this.props.item.name}</div>
                <div className="col-1 text-left">{this.props.item.status}</div>
                <div className="col-2 text-left">{this.props.item.assignee}</div>
                <div className="col-1 text-left">{this.props.item.storyPoints}</div>
            </div>);
    }
}

class Task extends Component {
    constructor(props) {
        super(props);
        this.state = {
            project: {},
            cTaskRelTaskMaster: []
        };

        this.autosave = this.autosave.bind(this);
        this.handleUserInput = this.handleUserInput.bind(this);

        let splitted = this.props.location.pathname.split('/');
        let id = splitted[splitted.length - 1];

        fetch('/api/tasks/' + id, {
            method: 'GET',
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("token"),
            }
        })
            .then(response => {
                console.log(response);
                return response.json();
            })
            .then(json => {
                console.log(json);
                this.setState(json);
            });
    }

    handleUserInput = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value });

        this.autosave();
    }

    autosave() {
        fetch('/api/tasks/' + this.state.id, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem("token"),
            },
            body: JSON.stringify(this.state),
        })
    }

    render() {
        let that = this;
        let subtasks = this.state.cTaskRelTaskMaster.map(item => {
            return (
                <div
                    className="d-flex stm-subtask align-items-center justify-content-between"
                    id={item.id}
                >
                    <div className="" style={{width:"8%"}}>
                        <a href={"task/" + item.id}>
                            {item.taskSlave.code}
                        </a>
                    </div>
                    <div className="" style={{ textOverflow: "ellipsis", width: "40%" }}>
                        {item.taskSlave.name}
                    </div>
                    <div className="" style={{ width: "10%" }}>
                        <img src={item.taskSlave.type ? item.taskSlave.type.icon : ""} />
                    </div>
                    <div className="" style={{ width: "10%" }}>
                        {item.taskSlave.status ? item.taskSlave.status.name : ""}
                    </div>
                    <div className="" style={{ width: "12%" }}>
                        {item.relType}
                    </div>
                    <div className="" style={{ width: "10%" }}>
                        <Button
                            classNames="stm-btn-link"
                            caption="Удалить"
                            onClick={function () {
                                let newRel = that.state.cTaskRelTaskMaster.filter(elem => elem.id != item.id);

                                that.setState({
                                    cTaskRelTaskMaster: newRel
                                });

                                fetch('/api/TaskRels/' + item.id, {
                                    method: "DELETE",
                                    headers: {
                                        "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                    }
                                });
                            }}
                        />
                    </div>
                </div>
            );
        });

        return (
            <div>
                <div class="row mt-4">
                    <div class="col" style={{ fontSize: "16px" }}>
                        <a style={{fontSize: "16px"}} href={"/Project/" + this.state.project.id}>{this.state.project.name}</a> / <a style={{fontSize: "16px"}} href={"/Task/" + this.state.id}>{this.state.code}</a>
                    </div>
                </div>
                <Text
                    placeholder="Название"
                    classNames="stm-text-plain mt-3 stm-text-large"
                    name="name"
                    value={this.state.name}
                    onChange={this.handleUserInput}
                    onBlur={this.autosave}
                />

                <h5>Детали</h5>
                <div className="row">
                    <div className="col-2">
                        <EntitySelect
                            name="statusId"
                            classNames=""
                            caption="Статус"
                            captionDirection="left"
                            width="75%"
                            onChange={this.handleUserInput}
                            onBlur={this.autosave}
                            value={this.state.statusId}
                            getSource={function () {
                                return fetch('/api/taskstatus', {
                                    method: 'GET',
                                    headers: {
                                        "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                    }
                                }).then(response => response.json());
                            }}
                        />
                    </div>
                    <div className="col-1">
                        <img src={this.state.status ? this.state.status.icon : ""} alt="" />
                    </div>
                    <div className="col-3">
                        <Text
                            caption="План. начало"
                            type="date"
                            name="plannedStart"
                            classNames={this.state.plannedStart > this.state.plannedComplete ? " error" : ""}
                            captionDirection="left"
                            width="60%"
                            onChange={this.handleUserInput}
                            onBlur={this.handleUserInput}
                            value={this.state.plannedStart}
                        />
                    </div>
                    <div className="col-3">
                        <Text
                            caption="План. окончание"
                            type="date"
                            name="plannedComplete"
                            classNames={this.state.plannedStart > this.state.plannedComplete ? " error" : ""}
                            captionDirection="left"
                            width="60%"
                            onChange={this.handleUserInput}
                            onBlur={this.handleUserInput}
                            value={this.state.plannedComplete}
                        />
                    </div>
                    <div className="col-3">
                        <EntitySelect
                            name="createdById"
                            readonly="true"
                            classNames=""
                            caption="Создал"
                            captionDirection="left"
                            width="75%"
                            value={this.state.createdById}
                            getSource={function () {
                                return fetch('/api/users', {
                                    method: 'GET',
                                    headers: {
                                        "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                    }
                                }).then(response => response.json())
                                    .then(json => json.map(item => {
                                        return {
                                            id: item.id,
                                            name: item.fullName
                                        }
                                    }));
                            }}
                        />
                    </div>
                </div>

                <div className="row">
                    <div className="col-2">
                        <EntitySelect
                            name="typeId"
                            classNames=""
                            caption="Тип"
                            captionDirection="left"
                            width="75%"
                            onChange={this.handleUserInput}
                            onBlur={this.autosave}
                            value={this.state.typeId}
                            getSource={function () {
                                return fetch('/api/tasktypes', {
                                    method: 'GET',
                                    headers: {
                                        "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                    }
                                }).then(response => response.json());
                            }}
                        />
                    </div>
                    <div className="col-1">
                        <img src={this.state.type ? this.state.type.icon : ""} alt="" />
                    </div>
                    <div className="col-3">
                        <Text
                            caption="Факт. начало"
                            type="date"
                            name="factStart"
                            classNames={this.state.factStart > this.state.factComplete ? " error" : ""}
                            captionDirection="left"
                            width="60%"
                            onChange={this.handleUserInput}
                            onBlur={this.autosave}
                            value={this.state.factStart}
                        />
                    </div>
                    <div className="col-3">
                        <Text
                            caption="Факт. окончание"
                            type="date"
                            name="factComplete"
                            classNames={this.state.factStart > this.state.factComplete ? " error" : ""}
                            captionDirection="left"
                            width="60%"
                            onChange={this.handleUserInput}
                            onBlur={this.autosave}
                            value={this.state.factComplete}
                        />
                    </div>
                    <div className="col-3">
                        <EntitySelect
                            name="assigneeId"
                            classNames=""
                            caption="Ответственный"
                            captionDirection="left"
                            width="75%"
                            onChange={this.handleUserInput}
                            onBlur={this.autosave}
                            value={this.state.statusId}
                            getSource={function () {
                                return fetch('/api/users', {
                                    method: 'GET',
                                    headers: {
                                        "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                    }
                                }).then(response => response.json())
                                    .then(json => json.map(item => {
                                        return {
                                            id: item.id,
                                            name: item.fullName
                                        }
                                    }));
                            }}
                        />
                    </div>
                </div>

                <div className="row">
                    <div className="col-2">
                        <EntitySelect
                            name="priorityId"
                            classNames=""
                            caption="Приоритет"
                            captionDirection="left"
                            width="75%"
                            onChange={this.handleUserInput}
                            onBlur={this.autosave}
                            value={this.state.priorityId}
                            getSource={function () {
                                return fetch('/api/taskpriorities', {
                                    method: 'GET',
                                    headers: {
                                        "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                    }
                                }).then(response => response.json());
                            }}
                        />
                    </div>
                    <div className="col-2">
                        <img src={this.state.priority ? this.state.priority.icon : ""} alt="" />
                    </div>
                    <div className="col-5"></div>
                    <div className="col-3">
                        <Text
                            caption="Story Points"
                            name="storyPoints"
                            captionDirection="left"
                            width="75%"
                            type="number"
                            onChange={this.handleUserInput}
                            onBlur={this.autosave}
                            value={this.state.storyPoints}
                        />
                    </div>
                </div>

                <h5>Описание</h5>
                <div className="row">
                    <div className="col">
                        <TextArea
                            placeholder="Описание"
                            width="100%"
                            captionDirection="left"
                            name="description"
                            value={this.state.description}
                            //classNames="stm-text-plain"
                            onChange={this.handleUserInput}
                            onBlur={this.autosave}
                        />
                    </div>
                </div>
                <h5>Связанные задачи</h5>
                {this.state.id ? <SubtypePopupContainer parentId={this.state.id} /> : <div />}
                {this.state.id ? <RelatedTask masterId={this.state.id} /> : <div />}
                {this.state.id ? <Comments taskId={this.state.id} />: <div />}
            </div>
        )
    }
}

class RelatedTask extends Component {
    constructor(props) {
        super(props);

        this.state = {
            items: []
        };

        
    }

    componentDidMount() {
        fetch('api/taskrels/masterid/' + this.props.masterId, {
            method: "GET",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("token")
            }
        })
            .then(response => response.json())
            .then(json => this.setState({ items: json }));
    }

    render() {
        let that = this;
        return (
            <div>{this.state.items.map(item => {
                return (
                    <div
                        className="d-flex stm-subtask align-items-center justify-content-between"
                        id={item.id}
                    >
                        <div className="" style={{ width: "8%" }}>
                            <a href={"task/" + item.taskSlaveId}>
                                {item.taskSlave.code}
                            </a>
                        </div>
                        <div className="" style={{ textOverflow: "ellipsis", width: "40%" }}>
                            {item.taskSlave.name}
                        </div>
                        <div className="" style={{ width: "10%" }}>
                            <img src={item.taskSlave.type ? item.taskSlave.type.icon : ""} />
                        </div>
                        <div className="" style={{ width: "10%" }}>
                            {item.taskSlave.status ? item.taskSlave.status.name : ""}
                        </div>
                        <div className="" style={{ width: "12%" }}>
                            {item.relType}
                        </div>
                        <div className="" style={{ width: "10%" }}>
                            <Button
                                classNames="stm-btn-link"
                                caption="Удалить"
                                onClick={function () {
                                    let newRel = that.state.items.filter(elem => elem.id != item.id);

                                    that.setState({
                                        items: newRel
                                    });

                                    fetch('/api/TaskRels/' + item.id, {
                                        method: "DELETE",
                                        headers: {
                                            "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                        }
                                    });
                                }}
                            />
                        </div>
                    </div>
                );
            })
            }</div>
        );
    }
}

class SubtypePopupContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            popupOpen: false,
        }
    }

    render() {
        let that = this;
        return (
            <div className="mb-2">
                <Button classNames="stm-btn-thin" caption="Добавить" onClick={() => this.setState({ popupOpen: true })} />
                <Popup
                    title="Создать задачу"
                    addToSubmit={(obj) => {
                        obj.taskMasterId = that.props.parentId;

                        return obj;
                    }} action="api/taskrels" method="POST" isOpen={this.state.popupOpen} onOk={(e) => {
                        this.setState({ popupOpen: false });
                    }}
                    onCancel={(e) => {
                        this.setState({ popupOpen: false })
                    }}>
                    <EntitySelect
                        name="relType"
                        caption="Тип связи"
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            let promise = new Promise(function(resolve, reject){
                                resolve([
                                    { name: "Блокирует", id: "Блокирует" },
                                    { name: "Блокируется", id: "Блокируется" },
                                    { name: "Подзадача", id: "Подзадача" },
                                    { name: "Родительская задача", id: "Родительская задача" }]);
                            });

                            return promise;
                        }}
                    />
                    <EntitySelect
                        name="taskSlaveId"
                        caption="Задача"
                        captionDirection="left"
                        width="75%"
                        getSource={function () {
                            return fetch('/api/tasks/LiteNotRels/' + that.props.parentId, {
                                method: 'GET',
                                headers: {
                                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                                }
                            }
                            ).then(r => r.json())
                            .then(json => json.map(item => {
                                return {
                                    id: item.id,
                                    name: (item.code ? item.code : "") + " " + item.name
                                }
                            }))
                        }}
                    />
                </Popup>
            </div>
        );
    }
}

class Comments extends Component {
    constructor(props) {
        super(props);
        this.state = {
            comments: []
        };
        fetch("/api/comments/task/" + this.props.taskId, {
            method: 'GET',
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("token")
            }
        }).then(response => response.json())
            .then(json => {
                this.setState({
                    comments: json,
                });
            })
    }

    render() {
        let that = this;
        return (
            <div className="stm-comments">
                <h4>Комментарии</h4>
                {this.state.comments.map(c => {
                    let closeBtn = c.userId == sessionStorage.getItem("id") ? (<span onClick={function () {
                        fetch("/api/comments/" + c.id, {
                            method: "DELETE",
                            headers: {
                                "Authorization": "Bearer " + sessionStorage.getItem("token")
                            }
                        });

                        let cmnts = that.state.comments.filter(item => item.id != c.id);
                        that.setState({ comments: cmnts });
                    }} className="stm-close"></span>) : null;
                    return (<div id={c.id} className="stm-comment mb-4">
                        <div className="row">
                            <div className="col mb-3"><span className="stm-comment-author">{c.user.fullName}</span> оставил(а) комментарий {Moment(c.created).format('L')}</div>
                        </div>
                        <div className="stm-comment-text">
                            {c.comment}
                        </div>
                        {closeBtn}
                    </div>)
                })
                }
                <CommentPopupContainer taskId={this.props.taskId} />
            </div>
        )
    }
}

class CommentPopupContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            popupOpen: false,
        }
    }

    render() {
        let that = this;
        return (
            <div className="mb-2">
                <Button classNames="stm-btn-thin stm-btn-blue" caption="Добавить комментарий" onClick={() => this.setState({ popupOpen: true })} />
                <Popup
                    title="Комментарий"
                    addToSubmit={(obj) => {
                        obj.taskId = that.props.taskId;
                        obj.userId = sessionStorage.getItem("id");

                        return obj;
                    }} action="api/comments" method="POST" isOpen={this.state.popupOpen} onOk={(e) => {
                        this.setState({ popupOpen: false });
                    }}
                    onCancel={(e) => {
                        this.setState({ popupOpen: false })
                    }}>
                    <TextArea
                        name="comment"
                        placeholder="Введите комментарий..."
                        captionDirection="left"
                        width="100%"
                    />
                </Popup>
            </div>
        );
    }
}


class AccountLogin extends Component {
    constructor(props) {
        super(props);
        this.state = {
            login: "",
            password: ""
        };
        this.handleUserInput = this.handleUserInput.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    handleUserInput = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value });
    }

    onSubmit = (e) => {
        fetch("auth/login", {
            method: "POST",
            body: JSON.stringify(this.state),
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem("token"),
            },
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                }

                response.text().then(message => swal({
                    title: "Ошибка",
                    text: message,
                    icon: "error",
                }));
                throw new Error("Breaked");
            })
            .then(json => {
                sessionStorage.setItem("token", json.access_token);
                sessionStorage.setItem("id", json.userId);
                sessionStorage.setItem("user", json.username);
                window.location.href = window.location.origin;
            })
            .catch(error => {
                console.log(error.message);
            });

        e.preventDefault();
    }

    render() {
        return (
            <div>
                <div className="login-form mt-5">
                    <div className="d-flex justify-content-center">
                        <h4>Авторизация</h4>
                    </div>
                    <form onSubmit={this.onSubmit}>
                        <div className="row mb-3">
                            <div className="col">
                                <Text placeholder="Логин" name="login" value={this.state.login} onChange={this.handleUserInput} />
                            </div>
                        </div>
                        <div className="row mb-3">
                            <div className="col">
                                <Text placeholder="Пароль" name="password" type="password" value={this.state.value} onChange={this.handleUserInput} />
                            </div>
                        </div>
                        <div className="d-flex justify-content-center">
                            <input type="submit" className="stm-btn" value="Войти" />
                        </div>
                    </form>
                </div>

                <div className="login-form mt-3">
                    <div className="text-center">
                        У вас еще нет аккаунта? <a href="account/register" > зарегистрироваться</a>
                    </div>
                </div>
            </div>
        )
    }
}

class AccountRegister extends Component {
    constructor(props) {
        super(props);
        this.state = {
            login: "",
            password: "",
            firstName: "",
            lastName: "",
            email: "",
            confirmPassword: "",
        };
        this.handleUserInput = this.handleUserInput.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    handleUserInput = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value });
    }

    onSubmit = (e) => {
        fetch("auth/register", {
            method: "POST",
            body: JSON.stringify(this.state),
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem("token"),
            },
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                }

                response.text().then(message => swal({
                    title: "Ошибка",
                    text: message,
                    icon: "error",
                }));
                throw new Error("Breaked");
            })
            .then(json => {
                sessionStorage.setItem("token", json.access_token);
                sessionStorage.setItem("id", json.userId);
                sessionStorage.setItem("user", json.username);
                window.location.href = window.location.origin;
            })
            .catch(error => {
                console.log(error.message);
            });

        e.preventDefault();
    }

    render() {
        return (
            <div className="login-form mt-5">
                <div className="d-flex justify-content-center">
                    <h4>Авторизация</h4>
                </div>
                <form onSubmit={this.onSubmit} >
                    <div className="row mb-3">
                        <div className="col">
                            <Text type="text" classNames="stm-text" name="login" value={this.state.login} onChange={this.handleUserInput} placeholder="Логин" />
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col">
                            <Text type="password" classNames="stm-text" name="password" value={this.state.password} onChange={this.handleUserInput} placeholder="Пароль" />
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col">
                            <Text type="password" classNames="stm-text" name="confirmPassword" value={this.state.confirmPassword} onChange={this.handleUserInput} placeholder="Подтвердите пароль" />
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col">
                            <Text type="text" classNames="stm-text" name="email" value={this.state.email} onChange={this.handleUserInput} placeholder="Email" />
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col">
                            <Text type="text" classNames="stm-text" name="firstName" value={this.state.firstName} onChange={this.handleUserInput} placeholder="Имя" />
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col">
                            <Text type="text" classNames="stm-text" name="lastName" value={this.state.lastName} onChange={this.handleUserInput} placeholder="Фамилия" />
                        </div>
                    </div>
                    <div className="d-flex justify-content-center">
                        <input type="submit" classNames="stm-btn" value="Зарегистрироваться" />
                    </div>
                </form>
            </div>
        )
    }
}