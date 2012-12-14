using System;
using System.Collections.Generic;

namespace Entidades
{
    [Serializable()]
    public class CentroComputo
    {
        private List<laboratorio> laboratorios = new List<laboratorio>();

        public List<laboratorio> Laboratorios
        {
            get { return laboratorios; }
            set { laboratorios = value; }
        }
    }
    [Serializable()]
    public sealed class laboratorio
    {
        private List<Equipo> equipos = new List<Equipo>();
        private int numeroLaboratorio;
        public laboratorio()
        {
            this.numeroLaboratorio = 0;
        }

        public laboratorio(int numeroLab)
        {
            this.numeroLaboratorio = numeroLab;
        }
        public int Numero
        {
            get { return numeroLaboratorio; }
            set { numeroLaboratorio = value; }
        }
        public List<Equipo> Equipos
        {
            set { equipos = value; }
            get { return equipos; }
        }
    }
    [Serializable()]
    public sealed class Equipo
    {
        int numero, x, y;
        public Equipo()
        { }
        public Equipo(int Numero, int X, int Y)
        {
            numero = Numero;
            x = X;
            y = Y;
        }
        public int Numero
        {
            set { numero = value; }
            get { return numero; }
        }
        public int X
        {
            set { x = value; }
            get { return x; }
        }
        public int Y
        {
            set { y = value; }
            get { return y; }
        }
    }
}
