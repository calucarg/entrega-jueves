main:
addi x11 x0 4 #a1 = 4
lw x12 0 x11 #a2 = load a1=0x4=0005a603
addi x13 x0 4 #a3 = 4
lw x13 0 x13 #a3 = load a1=0x4=0x0005a603
lw x13 0 x13 #a3 = load a1=0x0005a603=0 (no está def)
beq x12 x13 -20 #if(a2=a3)then jump -20 (14=20->jump a 0 = main)
#En este caso es FALSE ent sigue:
guardar:
lui x14 0xfffa6 #a4=0xfffa6000
addi x14 x14 -1539 #a4=0xfffa6000 - 1539 = 0xfff59fd
#Como 0x0005a603 = 370179 y 0xfff59fd = -370179:
add x12 x14 x12 #a2 = -370179 + 370179 = 0
sw x11 40 x12 #save a1=0x4 en 0x28+0x0=0x28
#acá PC=28 en memoria es 0x4
fin_programa:
addi x10 x0 0 #instrucción inutilizada
addi x17 x0 93 #a7 = 93
ecall #como a7=93 termina el programa