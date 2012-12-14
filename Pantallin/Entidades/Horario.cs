//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Entidades
{
    public partial class Horario
    {
        #region Primitive Properties
    
        public virtual int ID
        {
            get;
            set;
        }
    
        public virtual int IDAsignatura
        {
            get { return _iDAsignatura; }
            set
            {
                if (_iDAsignatura != value)
                {
                    if (Asignatura != null && Asignatura.ID != value)
                    {
                        Asignatura = null;
                    }
                    _iDAsignatura = value;
                }
            }
        }
        private int _iDAsignatura;
    
        public virtual string Dia
        {
            get;
            set;
        }
    
        public virtual string HoraInicio
        {
            get;
            set;
        }
    
        public virtual string HoraFin
        {
            get;
            set;
        }
    
        public virtual int IDLab
        {
            get { return _iDLab; }
            set
            {
                if (_iDLab != value)
                {
                    if (Laboratorio != null && Laboratorio.ID != value)
                    {
                        Laboratorio = null;
                    }
                    _iDLab = value;
                }
            }
        }
        private int _iDLab;
    
        public virtual int IDDocente
        {
            get { return _iDDocente; }
            set
            {
                if (_iDDocente != value)
                {
                    if (Docente != null && Docente.ID != value)
                    {
                        Docente = null;
                    }
                    _iDDocente = value;
                }
            }
        }
        private int _iDDocente;
    
        public virtual int IDGrupo
        {
            get { return _iDGrupo; }
            set
            {
                if (_iDGrupo != value)
                {
                    if (Grupo != null && Grupo.ID != value)
                    {
                        Grupo = null;
                    }
                    _iDGrupo = value;
                }
            }
        }
        private int _iDGrupo;

        #endregion

        #region Navigation Properties
    
        public virtual Laboratorio Laboratorio
        {
            get { return _laboratorio; }
            set
            {
                if (!ReferenceEquals(_laboratorio, value))
                {
                    var previousValue = _laboratorio;
                    _laboratorio = value;
                    FixupLaboratorio(previousValue);
                }
            }
        }
        private Laboratorio _laboratorio;
    
        public virtual Docente Docente
        {
            get { return _docente; }
            set
            {
                if (!ReferenceEquals(_docente, value))
                {
                    var previousValue = _docente;
                    _docente = value;
                    FixupDocente(previousValue);
                }
            }
        }
        private Docente _docente;
    
        public virtual Asignatura Asignatura
        {
            get { return _asignatura; }
            set
            {
                if (!ReferenceEquals(_asignatura, value))
                {
                    var previousValue = _asignatura;
                    _asignatura = value;
                    FixupAsignatura(previousValue);
                }
            }
        }
        private Asignatura _asignatura;
    
        public virtual Grupo Grupo
        {
            get { return _grupo; }
            set
            {
                if (!ReferenceEquals(_grupo, value))
                {
                    var previousValue = _grupo;
                    _grupo = value;
                    FixupGrupo(previousValue);
                }
            }
        }
        private Grupo _grupo;

        #endregion

        #region Association Fixup
    
        private void FixupLaboratorio(Laboratorio previousValue)
        {
            if (previousValue != null && previousValue.Horario.Contains(this))
            {
                previousValue.Horario.Remove(this);
            }
    
            if (Laboratorio != null)
            {
                if (!Laboratorio.Horario.Contains(this))
                {
                    Laboratorio.Horario.Add(this);
                }
                if (IDLab != Laboratorio.ID)
                {
                    IDLab = Laboratorio.ID;
                }
            }
        }
    
        private void FixupDocente(Docente previousValue)
        {
            if (previousValue != null && previousValue.Horario.Contains(this))
            {
                previousValue.Horario.Remove(this);
            }
    
            if (Docente != null)
            {
                if (!Docente.Horario.Contains(this))
                {
                    Docente.Horario.Add(this);
                }
                if (IDDocente != Docente.ID)
                {
                    IDDocente = Docente.ID;
                }
            }
        }
    
        private void FixupAsignatura(Asignatura previousValue)
        {
            if (previousValue != null && previousValue.Horario.Contains(this))
            {
                previousValue.Horario.Remove(this);
            }
    
            if (Asignatura != null)
            {
                if (!Asignatura.Horario.Contains(this))
                {
                    Asignatura.Horario.Add(this);
                }
                if (IDAsignatura != Asignatura.ID)
                {
                    IDAsignatura = Asignatura.ID;
                }
            }
        }
    
        private void FixupGrupo(Grupo previousValue)
        {
            if (previousValue != null && previousValue.Horario.Contains(this))
            {
                previousValue.Horario.Remove(this);
            }
    
            if (Grupo != null)
            {
                if (!Grupo.Horario.Contains(this))
                {
                    Grupo.Horario.Add(this);
                }
                if (IDGrupo != Grupo.ID)
                {
                    IDGrupo = Grupo.ID;
                }
            }
        }

        #endregion

    }
}
