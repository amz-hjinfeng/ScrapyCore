class scrapyTaskListTable {

    constructor(data, table) {
        this.data = data;
        this.table = table;
    }

    Render() {
        for (var i = 0; i < this.data.length; i++) {
            this.RenderRow(this.data[i]);
        }
    }
    RenderRow(datRow) {
        var row = $("<tr></tr>");
        this.table.append(row);
        this.RenderCheckBox(row, datRow);
        this.RenderId(row, datRow);
        this.RenderName(row, datRow);
        this.RenderStatus(row, datRow);
        this.RenderStartTime(row, datRow);
        this.RenderNumbers(row, datRow);
        this.RenderButtons(row, datRow);
    }
    RenderCheckBox(tbrow, dat) {
        tbrow.append("<td><input type='checkbox'></td>");
    }
    RenderId(tbrow, dat) {
        var idtd = tbrow.append("<td>" + dat.id + "</td>");
    }
    RenderName(tbrow, dat) {
        tbrow.append("<td>" + dat.name + "</td>");
    }
    RenderStatus(tbrow, dat) {
        tbrow.append("<td>" + dat.status + "</td>");
    }
    RenderStartTime(tbrow, dat) {
        tbrow.append("<td>" + dat.startTime + "</td>");
    }
    RenderNumbers(tbrow, dat) {
        tbrow.append("<td>" + dat.completed + "/" + dat.subTask + "</td>");
    }
    RenderButtons(tbrow, dat) {
        var btnBar = $("<div class='am-btn-toolbar'></div>");
        var btnGroup = $("<div class='am-btn-group am-btn-group-xs'></div>");
        tbrow.append(btnBar);
        btnBar.append(btnGroup);
        btnGroup.append("<button class='am-btn am-btn-default am-btn-xs am-text-secondary'><span class='am-icon-pencil-square-o'></span> 查看</button>");
        btnGroup.append("<button class='am-btn am-btn-default am-btn-xs am-hide-sm-only'><span class='am-icon-copy'></span> 复制</button>");
        btnGroup.append("<button class='am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only'><span class='am-icon-trash-o'></span> 取消</button>");
    }
}
