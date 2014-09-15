using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class 通用二元容器<类型A, 类型B>
{
    public 通用二元容器()
    {

    }

    public 通用二元容器(类型A 属性A)
        : this()
    {
        this.属性A = 属性A;
    }

    public 通用二元容器(类型A 属性A, 类型B 属性B)
        : this(属性A)
    {
        this.属性B = 属性B;
    }

    public 通用二元容器(通用二元容器<类型A, 类型B> 二元容器)
        : this(二元容器.属性A, 二元容器.属性B)
    {

    }

    public 类型A 属性A
    {
        get;
        set;
    }

    public 类型B 属性B
    {
        get;
        set;
    }
}

public class 通用三元容器<类型A, 类型B, 类型C> : 通用二元容器<类型A, 类型B>
{
    public 通用三元容器()
        : base()
    {

    }

    public 通用三元容器(类型A 属性A)
        : base(属性A)
    {

    }

    public 通用三元容器(类型A 属性A, 类型B 属性B)
        : base(属性A, 属性B)
    {

    }

    public 通用三元容器(通用二元容器<类型A, 类型B> 二元容器)
        : base(二元容器)
    {

    }

    public 通用三元容器(类型A 属性A, 类型B 属性B, 类型C 属性C)
        : this(属性A, 属性B)
    {
        this.属性C = 属性C;
    }

    public 通用三元容器(通用二元容器<类型A, 类型B> 二元容器, 类型C 属性C)
        : this(二元容器.属性A, 二元容器.属性B, 属性C)
    {

    }

    public 通用三元容器(通用三元容器<类型A, 类型B, 类型C> 三元容器)
        : this(三元容器.属性A, 三元容器.属性B, 三元容器.属性C)
    {

    }

    public 类型C 属性C
    {
        get;
        set;
    }
}

public class 通用四元容器<类型A, 类型B, 类型C, 类型D> : 通用三元容器<类型A, 类型B, 类型C>
{
    public 通用四元容器()
        : base()
    {

    }

    public 通用四元容器(类型A 属性A)
        : base(属性A)
    {

    }

    public 通用四元容器(类型A 属性A, 类型B 属性B)
        : base(属性A, 属性B)
    {

    }

    public 通用四元容器(通用二元容器<类型A, 类型B> 二元容器)
        : base(二元容器)
    {

    }

    public 通用四元容器(类型A 属性A, 类型B 属性B, 类型C 属性C)
        : base(属性A, 属性B, 属性C)
    {

    }

    public 通用四元容器(通用二元容器<类型A, 类型B> 二元容器, 类型C 属性C)
        : base(二元容器, 属性C)
    {

    }

    public 通用四元容器(通用三元容器<类型A, 类型B, 类型C> 三元容器)
        : base(三元容器.属性A, 三元容器.属性B, 三元容器.属性C)
    {

    }

    public 通用四元容器(类型A 属性A, 类型B 属性B, 类型C 属性C, 类型D 属性D)
        : this(属性A, 属性B, 属性C)
    {
        this.属性D = 属性D;
    }

    public 通用四元容器(通用二元容器<类型A, 类型B> 二元容器, 类型C 属性C, 类型D 属性D)
        : this(二元容器.属性A, 二元容器.属性B, 属性C, 属性D)
    {

    }

    public 通用四元容器(通用三元容器<类型A, 类型B, 类型C> 三元容器, 类型D 属性D)
        : this(三元容器.属性A, 三元容器.属性B, 三元容器.属性C, 属性D)
    {

    }

    public 通用四元容器(通用四元容器<类型A, 类型B, 类型C, 类型D> 四元容器)
        : this(四元容器.属性A, 四元容器.属性B, 四元容器.属性C, 四元容器.属性D)
    {

    }

    public 类型D 属性D
    {
        get;
        set;
    }
}

