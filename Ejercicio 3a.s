main:
addi x11 x0 4 #a1 = 4
lw x12 0 x11 #a2 = load 0x4 = 0 
addi x13 x0 4 #a3 = 4
lw x13 0 x13 #a3 = load 0x4 = 0
lw x13 0 x13 #a3 = load 0x4 = 0
beq x12 x13 -20 #if(a2=a3)then jump -20 (1c=28->jump a 8 = main)
#hay un bucle infinito si empieza en 08 :D
guardar:
lui x14 0xfffa6
addi x14 x14 -1539
add x12 x14 x12
sw x11 40 x12
fin_programa:
addi x10 x0 0
addi x17 x0 93
ecall