li a0,4228 #a0=4228
srai a1, a0, 1
jal ra, resta #ra = PC+4 = 0x14 y jump a resta
fin: beq zero, zero, fin # if (0=0) then jump fin (bucle infinito)
resta:
prologo:
addi sp, sp,-4 #hago un byte de espacio en el stack pointer
sw ra,0(sp) #guardo ra en el sp
sub a0,a0,a1 #a0=a0-a1
beq a0,zero,epilogo #if(a0=0) then jump epilogo
sigo: jal ra, resta #ra = PC+4 = 0x2c y jump a resta
epilogo:
lw ra, 0(sp) #ra= ultimo valor en la pila
addi sp, sp,4 #devuelvo el espacio que usaba ese valor
ret #return a ra (loop desde lw a ret hasta ra=0x14 ent salta a fin)