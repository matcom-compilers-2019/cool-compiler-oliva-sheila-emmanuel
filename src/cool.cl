class Main inherits IO{
c:Int;
main():Object {{
    let x : A <- new A in
        c <- case x of
                y : Object => 1;
                z : B => 2;
                esac;
        out_int(c);
}
};

};

class A inherits B{};

class B {};
