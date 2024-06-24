esta O(n)
nuevoDiccionario O(1)
obtener O(n)
definir O(n)
borrar O(n)
cantSignificados O(n)

Modulo DicHistorico implementa DiccionarioConHistoria<K, V> {
    var dict : DiccionarioLineal<K,tupla(V,int)>

    impl nuevoDiccionario() : DiccionarioLineal<K,tupla(V,int)> {
        return nuevo DiccionarioLineal<K,tupla(V,int)> //O(1)
    }

    impl esta (in d: DicHistorico, in k:K) : bool {
        if (d.está(k)) { //O(n)
            return true //O(1)
        }
    } //O(n)

    impl definir (inout d: DicHistorico, in k:K, in v:V) {
        if (!d.está(k)) { //O(n)
            d.definirRapido(k) = (v,0) //O(1)
        } else {
            d.definir(k)[0] = v //O(n) 
            d.definir(k)[1] = d.definir(k)[0] + 1 //O(n)
        } //O(n+1+n+n) ≡ O(n)
    }

    impl obtener (in d: DicHistorico, in k:K, in v:V) ...

    impl borrar (inout d:DicHistorico, in k:k) : V {
        int res = d.dict.obtener(k)[0] //O(n)
        d.dict.borrar(k) //O(n)
        return res //O(1)
    } //O(n)

    impl cantSignificados (in d:DicHistorico, in k:K) : int {
        return d.dict.obtener(k)[1] //O(n)
    }
}

2) Cambiaría la estructura a:
        var dict : DiccionarioLineal<K,tupla(V,LE)>, no cambian las complejidades de los algoritmos actuales.

6. Modulo Ingresos implementa IngresosAlBanco {
    var totales : vector<int>

    proc nuevoIngresos(): Ingresos {
        res = new Ingresos
        res.totales = new vectorVacio
        res.totales.agregarAtras[0]
        return res
    }

    proc registrarNuevoDia(inout i: Ingresos, in cant: Z) {
        ingresosActuales = i.totales.obtener(longitud-1) //O(n)
        i.totales.agregarAtras(cant+ingresosActuales) //O(n)
        return i //O(1)
    } //O(n)

    proc cantDias(in i: Ingresos): Z {
        return i.totales.longitud //O(1)
    }

    proc cantPersonas(in i: Ingresos, in desde: Z, in hasta: Z): Z {
        return i.obtener(hasta) - i.obtener(desde) //O(n+n) ≡ =(n)
    }

    proc mediana(in i: Ingresos) : int {
        for(j=0,j<i.totales.tamaño,j++) {
            m = i.totales.tamaño-j-2
            if(cantPersonas(i,0,cantDias(m)≤cantPersonas(i,m+1,cantDias(i)))){
                return m
            }
        }
        return -1
    }
}


20.
Modulo Biblioteca implementa TADBiblioteca:
/*
    L es la cantidad de libros en la coleccion
    r es la cantidad de libros que el socio en cuestion tiene retirados
    k la cantidad de posiciones libres en la estanteria
*/
Posicion es int
Libro es int
Socio es String(50)
var estantes: DiccionarioLog<Libro,Posicion>
var socios: DiccionarioDigital<Socio,ConjuntoLog<Libro>>
var disponibles: ColaDePrioridadLog<Posicion>
var len: int

proc nuevaBiblioteca(in size: int): Biblioteca {
    res = new Biblioteca
    res.estantes = new DiccionarioLog<Libro,Posicion>
    res.socios = new DiccionarioDigital<Socio,ConjuntoLog<idLibro>>
    res.disponibles = new ConjuntoLog<Posicion>
    for (int i = 0, i < res.len, i++ ) {
        res.disponibles.agregarRapido(i)
    }
    return res
}

proc AgregarLibroAlCatalogo(inout b: Biblioteca, in l: idLibro) {
    int min = b.disponibles.buscarMin() //O(log k)
    b.disponibles.sacar(min) //O(log k)

    b.estantes.definir(libro,min) // O(log L)

    return b //O(1)

} //O(log k + log L)

proc PedirLibro(inout b: Biblioteca, in l: idLibro, in s: Socio) {
    <idLibro,Posicion> libro = b.estantes.obtener(l) //O(log L)
    b.estantes.borrar(l) //O(log L)

    b.socios.definir(libro[0]) //O(log r)
    b.disponibles.agregar(libro[1]) //O(log k)

    return b

} //O(log r + log k + log L)

proc DevolverLibro(inout b: Biblioteca, in l: idLibro, in s: Socio) {
    b.socios.obtener(socio).sacar(l) //O(1) + O(log r)
    int min = b.disponibles.buscarMin() //O(log k)
    b.estantes.definir(l,min) //O(log L)

    return b

}//O(log r + log k + log L)

proc Prestados(in b: Biblioteca, in s: Socio): Conjunto<Libro> {
    return b.socios.obtener(s) //O(50) = O(1)
} //O(1)

proc UbicacionDeLibro(in b: Biblioteca, in l: idLibro): Posicion {
    b.estantes.obtener(l) //O(log L)
    return l[1] //O(1)
} //O(log L)

Modulo Agenda {

    tags: DiccionarioDigital<Tag,ConjuntoLineal<IdActividad>>
    actividades: DiccionarioLog<IdActividad,struct<dia:Dia,inicio:Hora,fin:Hora>>
    dias: DiccionarioLog<Dia,IdActividad>


    pred invRep(ag: Agenda) {
        si k es clave de actividades entonces k existe en algun dias[dia] y algun tags[tag]
        si d existe en algun actividad entonces d existe en dias
    }

    proc RegistrarActividad(inout ag: Agenda, in act: IdActividad, in dia: Dia, in inicio: Hora, in fin: Hora) {
        ag.actividades.definir(act,struct<Dia,Hora,Hora>) //O(log a)
        if(!ag.dias.esta(dia)) {
            ag.dias.definir(dia, vector<int>(24))
        }
        ag.dias.obtener(dia)[horaAint(inicio)] = ag.dias.obtener(dia)[horaAint(inicio)] + 1 //O(log d) + O(1)
    } //O(log(a) + log(d))

    proc VerActividad(in ag: Agenda, in act: IdActividad): struct<dia: Dia, inicio: Hora, fin: Hora> {
        return ag.actividades.obtener(act) //O(log a)
    } //O(log(a))

    proc AgregarTag(inout ag: Agenda, in act: IdActividad, in t: Tag) {
        if(!ag.tags.esta(t)) { //O(1)
            ag.tags.definir(t, new ConjuntoLineal<IdActividad>) //O(20) = O(1)
        }
        ag.tags.obtener(t).agregar(act) //O(1) + O(1) = O(1)
        
    } //O(1)

    proc HoraMasOcupada(in ag: Agenda, in d: Dia): Hora {
        lista = ag.dias.obtener(d) //O(log d)
        int max= -1 
        for (i=0,i<24,i++) { //O(24)
            if (lista[i] > max) {
                max = lista[i]
            }
        }
        return intToHora(max)
    } //O(log(d))

    proc ActividadesPorTag(in ag: Agenda, in t: Tag): Conjunto<IdActividad> {
        return ag.tags.obtener(t) //O(20) = //O(1)
    } //O(1)
}

Modulo fulbot {
    posiciones: ConjuntoLog<Puntos> //Hay a lo sumo |puntos|=|equipos|=n claves
    equipos: DiccionarioLog<Equipo,Puntos>

    proc registrarPartido(inout t: Torneo, in ganador: Equipo, in perdedor: Equipo) {
        if(!t.equipos.esta(ganador)) { //O(log n)
            t.equipos.definir(ganador,0) //O(log n)
        }
        if(!t.equipos.esta(perdedor)) { //O(log n)
            t.equipos.definir(perdedor,0) //O(log n)
        }
        t.equipos.definir(ganador,t.equipos.obtener(ganador) + 1) //O(2log n)
        
        t.posiciones.agregar(ganador,t.equipos.obtener(ganador) + 1) //O(2log n)
    } //O(8log n) ≡ O(log n)

    proc posicion(in t: Torneo, in e: Equipo): int {
        int puntuacion = t.equipos.obtener(e) //O(log n)
        Nodo actual = t.posiciones.elems.raiz
        int i = 0
        while(puntuacion != actual.valor) { //O(log n) (va dividiendo entre 2)
            if(puntuacion < actual.valor) {
                actual = actual.izquierda
            } else {
                actual = actual.derecha
            }
            i++
        }
        return i
    } //O(2*log n) ≡ O(log n)

    proc puntos(in t: Torneo, in e: Equipo): int {
        return t.equipos.obtener(e) //O(log n)
    }
}