#!/usr/bin/env python3

import os.path
import platform
import random


def greeting():
    print()
    print("                       .,o'       `o,.")
    print("                     ,o8'           `8o.")
    print("                    o8'               `8o")
    print("                   o8:                 ;8o")
    print("                  .88                   88.")
    print("                  :88.                 ,88:")
    print("                  `888                 888'")
    print("                   888o   `888 888'   o888")
    print("                   `888o,. `88 88' .,o888'")
    print("                    `8888888888888888888'")
    print("                      `888888888888888'")
    print("                         `::88;88;:'")
    print("                            88 88")
    print("                            88 88")
    print("                            `8 8'")
    print("                             ` ' ")
    print("################################################################")
    print("#### ¡BIENVENIDO AL CONFIGURADOR BÁSICO DE QUAKE III ARENA! ####")
    print("################################################################")


def get_input_menu(options):
    for key, value in options.items():
        print(value)
    print("\nElige una opción del menú:")
    menu_input = input(">>> ")
    while menu_input not in options:
        print("Opción incorrecta, intentémoslo de nuevo")
        menu_input = input(">>> ")
    return int(menu_input)


def main_menu():
    options = {
        "1": "[1] - Configuración básica y guiada",
        "2": "[2] - Ejecutar Quake III Arena",
        "3": "[3] - Ejecutar Quake III Arena con mods",
        "0": "[0] - Salir del programa",
    }
    return get_input_menu(options)


def mod_menu():
    options = {
        "1": "[1] - Ejecutar Quake III Team Arena",
        "2": "[2] - Ejecutar OSP",
        "3": "[3] - Ejecutar CPMA",
        "0": "[0] - Salir de este menú"
    }
    print()
    print("##########################################")
    print("#### MENÚ PARA LANZAR QUAKE III ARENA ####")
    print("##########################################")
    return get_input_menu(options)


def launch_mod():
    autoexec_cfg = " +exec autoexec.cfg"
    arguments = {
        1: " +set fs_game missionpack" + autoexec_cfg,
        2: " +set fs_game osp" + autoexec_cfg,
        3: " +set fs_game cpma" + autoexec_cfg,
    }
    mod = mod_menu()
    if mod != 0:
        os.system(get_binary() + arguments[mod])


def write_player():
    print("\nEscribe el nombre que quieres tener en el juego")
    player_name = input(">>> ").strip()
    while player_name == "":
        print("No has escrito nada, intentémoslo de nuevo")
        player_name = input(">>> ").strip()
    return player_name


def write_sensitivity():
    print("\nEscribe un número decimal con máximo de 2 decimales, entre 1 y 5, más alto el valor, mayor sensibilidad")
    try:
        sensitivity = float(input(">>> ").replace(",", "."))
        while sensitivity < 1.00 or sensitivity > 5.00:
            print("El valor introducido no es correcto, intentémoslo de nuevo")
            sensitivity = float(input(">>> ").replace(",", "."))
        return str(sensitivity)
    except ValueError:
        print("El valor introducido no es correcto, intentémoslo de nuevo")
        write_sensitivity()


def write_model():
    model_list = [
        "anarki/blue",      "anarki/default",   "anarki/red",       "biker/blue",
        "biker/cadavre",    "biker/default",    "biker/hossman",    "biker/red",
        "biker/slammer",    "biker/stroggo",    "bitterman/blue",   "bitterman/default",
        "bitterman/red",    "bones/blue",       "bones/bones",      "bones/default",
        "bones/red",        "crash/blue",       "crash/default",    "crash/red",
        "doom/blue",        "doom/default",     "doom/phobos",      "doom/red",
        "grunt/blue",       "grunt/default",    "grunt/red",        "grunt/stripe",
        "hunter/blue",      "hunter/default",   "hunter/harpy",     "hunter/red",
        "keel/blue",        "keel/default",     "keel/red",         "klesk/blue",
        "klesk/default",    "klesk/flisk",      "klesk/red",        "lucy/angel",
        "lucy/blue",        "lucy/default",     "lucy/red",         "major/blue",
        "major/daemia",     "major/default",    "major/red",        "mynx/blue",
        "mynx/default",     "mynx/red",         "orbb/blue",        "orbb/default",
        "orbb/red",         "ranger/blue",      "ranger/default",   "ranger/red",
        "ranger/wrack",     "razor/blue",       "razor/default",    "razor/id",
        "razor/patriot",    "razor/red",        "sarge/blue",       "sarge/default",
        "sarge/krusade",    "sarge/red",        "slash/blue",       "slash/default",
        "slash/grrl",       "slash/red",        "slash/yuriko",     "sorlag/blue",
        "sorlag/default",   "sorlag/red",       "tankjr/blue",      "tankjr/default",
        "tankjr/red",       "uriel/blue",       "uriel/default",    "uriel/red",
        "uriel/zael",       "visor/blue",       "visor/default",    "visor/gorre",
        "visor/red",        "xaero/blue",       "xaero/default",    "xaero/red",
    ]
    return random.choice(model_list)


def write_file():
    autoexec_files = [
        "baseq3/autoexec.cfg.orig",
        "cpma/autoexec.cfg.orig",
        "missionpack/autoexec.cfg.orig",
        "osp/autoexec.cfg.orig",
    ]
    nickname = write_player()
    sensitivity = write_sensitivity()
    model = write_model()
    for autoexec_file in autoexec_files:
        new_file = autoexec_file.replace(".orig", "")
        cfg = open(autoexec_file, "r")
        cfg_data = cfg.read()
        cfg.close()
        cfg_data = cfg_data.replace("{quake_3_player}", nickname)
        cfg_data = cfg_data.replace("{quake_3_sensitivity}", sensitivity)
        cfg_data = cfg_data.replace("{quake_3_model}", model)
        new_cfg = open(new_file, "w")
        new_cfg.write(cfg_data)
        new_cfg.close()


def find_binary():
    platforms = {
        "Linux":    "quake3-linux",
        "Windows":  "quake3.exe",
        "Darwin":   "quake3-mac"
    }
    return platforms[platform.system()]


def get_binary():
    basepath = os.path.dirname(os.path.abspath(__file__))
    cmd = os.path.join(basepath, find_binary())
    return '"' + cmd + '"'


def launch_game():
    os.system(get_binary())


if __name__ == "__main__":
    methods = {
        1: write_file,
        2: launch_game,
        3: launch_mod,
    }
    greeting()
    method = main_menu()
    while method != 0:
        call_method = methods[method]
        call_method()
        print()
        print("########################")
        print("#### MENÚ PRINCIPAL ####")
        print("########################")
        method = main_menu()
    print("\n¡HASTA LA PRÓXIMA!\n")
